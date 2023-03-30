using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{
    internal interface IClustering
    {
        public ClusteringResult GetClusters(DataTable<double> dataTable);
    }

    internal class TransitiveClustering : IClustering
    {
        private readonly IDistance _distance;

        private readonly double _criticalDistance;

        public TransitiveClustering(double criticalDistance, IDistance distance)
        {
            _criticalDistance = criticalDistance;
            _distance = distance;
        }

        public ClusteringResult GetClusters(DataTable<double> dataTable)
        {
            var distanceMatrix = new DistanceMatrix(dataTable, _distance);

            bool[,] binaryMatrix = GetBinaryMatrix(distanceMatrix);

            bool[,] closureMatrix = GetTransitiveMatrix(binaryMatrix);

            int[] clusterIndices = DeepFirstSearch(closureMatrix);
            return new ClusteringResult(clusterIndices);
        }

        private bool[,] GetBinaryMatrix(DistanceMatrix distancematrix)
        {
            bool[,] binaryMatrix = new bool[distancematrix.Size, distancematrix.Size];
            for (int i = 0; i < distancematrix.Size; i++)
            {
                for (int j = 0; j < distancematrix.Size; j++)
                {                    
                    binaryMatrix[i, j] = distancematrix[i,j]<=_criticalDistance;
                }
            }
            return binaryMatrix;
        }

        private bool[,] GetTransitiveMatrix(bool[,] binaryMatrix)
        {
            bool[,] closureMatrix = new bool[binaryMatrix.GetLength(0), binaryMatrix.GetLength(0)];

            for (int i = 0; i < binaryMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < binaryMatrix.GetLength(0); j++)
                {
                    closureMatrix[i, j] = binaryMatrix[i, j];
                }
            }

            bool converged;

            do
            {
                converged = true;

                for (int i = 0; i < closureMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < closureMatrix.GetLength(0); j++)
                    {
                        for (int k = 0; k < closureMatrix.GetLength(0); k++)
                        {
                            if (!closureMatrix[i, j])
                            {
                                if (closureMatrix[i, k] && closureMatrix[k, j])
                                {
                                    converged = false;
                                    closureMatrix[i, j] = true;
                                }
                            }
                        }
                    }
                }
            } while (!converged);
            return closureMatrix;
        }

        public int[] DeepFirstSearch(bool[,] closureMatrix)
        {
            int size = closureMatrix.GetLength(0);

            var visited = new bool[size];
            var clusterInidices = new int[closureMatrix.GetLength(0)];

            int clusterIndex = -1;
            for (int i = 0; i < size; i++)
            {
                if (!visited[i])
                {
                    clusterIndex++;
                    DFS(i, clusterIndex);
                }
            }

            return clusterInidices;

            void DFS(int vertex, int clusterIndex)
            {
                var stack = new Stack<int>();
                stack.Push(vertex);
                clusterInidices[vertex] = clusterIndex;

                if (!visited[vertex])
                {
                    visited[vertex] = true;

                    for (int i = 0; i < size; i++)
                    {
                        if (i != vertex && closureMatrix[vertex, i])
                        {
                            stack.Push(i);
                            clusterInidices[i] = clusterIndex;
                        }
                    }
                }
            }
        }
    }

    internal class ClusteringResult
    {
        private Dictionary<int, List<int>> Clusters;

        public int ClustersCount => Clusters.Count;
        public IReadOnlyCollection<int> ClusterIndices => Clusters.Keys;

        public ClusteringResult(int[] clusterIndices)
        {
            Clusters = new Dictionary<int, List<int>>();
            for (int i = 0;i < clusterIndices.Length;i++)
            {
                int key = clusterIndices[i];
                if(!Clusters.ContainsKey(key))
                {
                    Clusters[key] = new List<int>();
                }
                Clusters[key].Add(i);
            }

        }

        public IReadOnlyList<int> this[int clusterIndex]
        { 
            get { return Clusters[clusterIndex];}
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in Clusters.Keys)
            {
                sb.Append("# ");
                sb.Append(key);
                sb.Append(": ");
                foreach (var item in Clusters[key])
                {
                    sb.Append(item.ToString());
                    sb.Append('\t');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }

    internal class KMeansClustering : IClustering
    {
        private readonly IDistance _distance;

        private readonly int _clustersCount;

        public KMeansClustering(int clusterCount, IDistance distance)
        {
            _distance = distance;
            _clustersCount = clusterCount;            
        }

        public ClusteringResult GetClusters(DataTable<double> dataTable)
        {
            int[] clusterCenterIndices = GetRandomCenters(dataTable.RowCount, _clustersCount);
            List<double[]> clusterCentres = GetClusterCenters(dataTable, clusterCenterIndices);
            int flag = -1;
            int[] clusterIndices = new int[dataTable.RowCount];
            int[] clusterIndicesIter = new int[dataTable.RowCount];                
            clusterIndices.CopyTo(clusterIndicesIter, 0);
            do
            {               
                clusterIndices = DisributeToClusters(dataTable, clusterCentres);
                clusterCentres = CalculateNewCenters(clusterIndices, dataTable);
                if (clusterIndices.SequenceEqual(clusterIndicesIter))
                {
                    flag = 1;
                    break;
                }
                clusterIndices.CopyTo(clusterIndicesIter, 0);

            } while (flag < 0);

            return new ClusteringResult(clusterIndices);
        }

        private int[] GetRandomCenters(int n, int k)
        {
            Random random = new Random();
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }
            if (k < 0 || k > n)
            {
                throw new ArgumentOutOfRangeException(nameof(k));
            }

            var cmb = new HashSet<int>();

            for (int i = n - k; i < n; i++)
            {
                int j = random.Next(i + 1);

                cmb.Add(cmb.Contains(j) ? i : j);
            }

            return cmb.ToArray();
        }

        private List<double[]> GetClusterCenters(DataTable<double> dataTable, int[] clusterCenterIndices) 
        {
            List<double[]> clusterCenters = new List<double[]>();
            for (int i = 0; i < _clustersCount; i++)
            {
                clusterCenters.Add(new double[dataTable.RowCount]);
                clusterCenters[i] = dataTable.GetRow(clusterCenterIndices[i]).ToArray();
            }
            return clusterCenters;
        }

        private int[] DisributeToClusters(DataTable<double> dataTable, List<double[]> clusterCentres)
        {
            int[] Clusters = new int[dataTable.RowCount];



            for (int i = 0;i < dataTable.RowCount;i++) 
            {
                int index=0;
                double distance = double.MaxValue;
                for(int j = 0; j<_clustersCount;j++)
                {
                    if (_distance.GetDistance(dataTable.GetRow(i).ToArray(), clusterCentres[j])<distance)
                    {
                        distance = _distance.GetDistance(dataTable.GetRow(i).ToArray(), clusterCentres[j]);
                        index = j;
                    }                    
                }
                Clusters[i] = index;
            }
            return Clusters;
        }

        private List<double[]> CalculateNewCenters(int[] clusterIndices, DataTable<double> dataTable) 
        {
            List<double[]> clusterCenters = new List<double[]>();
            int[] elementsCount = new int[_clustersCount];
            for(int i = 0;i<_clustersCount;i++)
            {
                clusterCenters.Add(new double[dataTable.ColumnCount]);                              
            }

            
            for (int i = 0; i < dataTable.RowCount; i++)
            {
                for (int j = 0; j < _clustersCount; j++)
                {
                    if (clusterIndices[i]==j)
                    {
                        for (int k = 0; k <dataTable.ColumnCount; k++)
                        {
                            clusterCenters[j][k] += dataTable[i, k];
                        }
                        elementsCount[j]++;
                        
                    }
                }
            }

            for (int i = 0;i<clusterCenters.Count;i++) 
            { 
                for(int j = 0;j < clusterCenters[i].Length;j++)
                {
                    clusterCenters[i][j] /= elementsCount[i]; 
                }
            }


            return clusterCenters;
        }

    }
}

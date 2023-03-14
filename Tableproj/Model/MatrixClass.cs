using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{


    internal class MatrixClass
    {
        public double[,] DataMatrix { get; set; }

        public double[,] DistanceMatrix { get; set; }

        public double[,] SimilarityMatrix { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public List<List<int>> Groups { get; set; }

        public MatrixClass(int rows, int columns)
        {
            DataMatrix = new double[rows + 1, columns + 1];
            Rows = rows + 1;
            Columns = columns + 1;
            DistanceMatrix = new double[rows + 1, rows + 1];
            SimilarityMatrix = new double[rows + 1, rows + 1];
        }

        public void FillMartixRandom()
        {
            Random random = new Random();
            for (int i = 1; i < Rows; i++)
            {
                for (int j = 1; j < Columns; j++)
                {
                    DataMatrix[i, j] = random.Next(2);
                }
            }

            for (int i = 1; i < Rows; i++)
            {
                DataMatrix[i, 0] = i;
            }
            for (int i = 1; i < Columns; i++)
            {
                DataMatrix[0, i] = i;
            }
        }

        public void PrintDataMatrix()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(DataMatrix[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

        }

        public void PrintDistanceMatrix()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Console.Write(DistanceMatrix[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

        }

        public void PrintSimilarityMatrix()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Console.Write(SimilarityMatrix[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

        }

        public double Similarity(double[] row1, double[] row2, int method)
        {
            double p11 = 0;
            double p10 = 0;
            double p01 = 0;
            double p00 = 0;

            for (int i = 1; i < Columns; i++)
            {
                if (row1[i] == 1 && row2[i] == 1) p11++;
                else if (row1[i] == 1 && row2[i] != 1) p10++;
                else if (row2[i] == 1 && row1[i] != 1) p01++;
                else p00++;
            }

            if (method == 1) return Math.Round(p11 / (p11 + p10 + p01 + p00), 2);
            else if (method == 2) return Math.Round(((p11 + p00) / (p11 + p10 + p01 + p00)), 2);
            //else if (method == 3) return p11 / (p11 + p10 + p01);
            //else return (p11)
            else return Math.Round((p11 / (p11 + p10 + p01)), 2);
        }

        public double[] GetRow(int row)
        {
            double[] row1 = new double[Columns];
            for (int i = 1; i < Columns; i++)
            {
                row1[i] = DataMatrix[row, i];
            }
            return row1;
        }

        public void CalculateSimilarity()
        {

            for (int i = 0; i < Rows; i++)
            {
                SimilarityMatrix[i, 0] = i;
                SimilarityMatrix[0, i] = i;
            }

            for (int i = 1; i < Rows; i++)
            {
                for (int j = 1; j < Rows; j++)
                {
                    SimilarityMatrix[i, j] = Similarity(GetRow(i), GetRow(j), 3);
                }
            }
        }

        public double Distance(double[] row1, double[] row2)
        {
            double sum = 0;
            for (int i = 1; i < Columns; i++)
            {
                sum += Math.Pow((row1[i] - row2[i]), 2);
            }
            return Math.Round(Math.Sqrt(sum), 2);
        }

        public void CalculateDistance()
        {

            for (int i = 0; i < Rows; i++)
            {
                DistanceMatrix[i, 0] = i;
                DistanceMatrix[0, i] = i;
            }

            for (int i = 1; i < Rows; i++)
            {
                for (int j = 1; j < Rows; j++)
                {
                    DistanceMatrix[i, j] = Distance(GetRow(i), GetRow(j));
                }
            }
        }

        public void SetCriticalDistance(double criticalDistance)
        {
            for (int i = 1; i < Rows; i++)
            {
                for (int j = 1; j < Rows; j++)
                {
                    if (DistanceMatrix[i, j] <= criticalDistance)
                    {
                        DistanceMatrix[i, j] = 1;
                    }
                    else { DistanceMatrix[i, j] = 0; }
                }
            }
        }

        public void TransitiveClosure()
        {
            int i, j, k;

            for (k = 1; k < Rows; k++)
            {
                for (i = 1; i < Rows; i++)
                {
                    for (j = 1; j < Rows; j++)
                    {
                        if (DistanceMatrix[i, k] == 1 && DistanceMatrix[k, j] == 1)
                        {
                            DistanceMatrix[i, j] = 1;
                        }
                    }
                }
            }
        }

        public void DFS(bool[] visited, int i, List<int> group)
        {
            visited[i] = true;
            group.Add(i);

            for (int j = 1; j < visited.Length; j++)
            {
                if (DistanceMatrix[i, j] == 1 && !visited[j])
                {
                    DFS(visited, j, group);
                }
            }
        }

        public void FindGroups()
        {
            Groups = new List<List<int>>();
            bool[] visited = new bool[Rows];

            for (int i = 1; i < Rows; i++)
            {
                if (!visited[i - 1])
                {
                    List<int> group = new List<int>();
                    DFS(visited, i, group);
                    Groups.Add(group);
                }
            }
        }

        public void PrintGroups()
        {
            foreach (List<int> group in Groups)
            {
                foreach (int i in group)
                {
                    Console.Write($"{i}\t");
                }
                Console.WriteLine();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{
    internal class DepthFirstSearch
    {
        public static int[] GetClusters(bool[,] closureMatrix)
        {
            int size = closureMatrix.GetLength(0);

            var visited = new bool[size];
            var clusterInidices = new int[size];

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
                            stack.Push(vertex);
                            clusterInidices[vertex] = clusterIndex;
                        }
                    }
                }
            }
        }
    }
}

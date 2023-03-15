using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tableproj.Model;

namespace Tableproj.Controller
{
    internal class DeepFind
    {
        public List<List<int>> Groups { get; set; }
        public DeepFind() 
        { 
            Groups = new List<List<int>>();
        }
        public void DFS(IMatrix matrix, bool[] visited, int i, List<int> group)
        {
            visited[i] = true;
            group.Add(i);

            for (int j = 1; j < visited.Length; j++)
            {
                if (matrix.GetBinaryMatrix()[i, j] && !visited[j])
                {
                    DFS(matrix, visited, j, group);
                }
            }
        }

        public void FindGroups(IMatrix matrix)
        {
            Groups = new List<List<int>>();
            bool[] visited = new bool[matrix.GetRowCount()];

            for (int i = 1; i < matrix.GetRowCount(); i++)
            {
                if (!visited[i - 1])
                {
                    List<int> group = new List<int>();
                    DFS(matrix, visited, i, group);
                    Groups.Add(group);
                }
            }
        }

    }
}

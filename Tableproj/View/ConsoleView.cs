using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tableproj.Controller;
using Tableproj.Model;

namespace Tableproj.View
{
    internal static class ConsoleView
    {
        public static void PrintBinaryMatrix(IMatrix matrix)
        {
            for (int i = 0; i < matrix.GetRowCount()+1; i++) 
            { 
                if(i == 0)
                {
                    for(int j = 0; j < matrix.GetRowCount() +1; j++) 
                    {
                        Console.Write($"{j}\t");                     
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"{i}\t");
                    for (int k = 0; k < matrix.GetColumnCount(); k++)
                    {
                        if(matrix.GetBinaryMatrix()[i - 1, k])
                            Console.Write("1\t");
                        else
                            Console.Write("0\t");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void PrintDoubleMatrix(IMatrix matrix)
        {
            for (int i = 0; i < matrix.GetRowCount() + 1; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < matrix.GetColumnCount() + 1; j++)
                    {
                        Console.Write($"{j}\t");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"{i}\t");
                    for (int k = 0; k < matrix.GetColumnCount(); k++)
                    {
                        Console.Write($"{matrix.GetDoubleMatrix()[i-1,k]}\t");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void PrintGroups(List<List<int>> groups)
        {
            foreach (List<int> group in groups)
            {
                Console.Write("Group:\t");
                foreach (int i in group)
                {
                    Console.Write($"{i}\t");
                }
                Console.WriteLine();
            }
        }

    }
}

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
        public static void PrintTable(DataTable<double> table)
        {
            Console.WriteLine(table.ToString());
        }

        

        public static void PrintGroups(List<int> groups)
        {
            foreach (int group in groups)
            {
                Console.Write(group+"\t");
            }
        }

    }
}

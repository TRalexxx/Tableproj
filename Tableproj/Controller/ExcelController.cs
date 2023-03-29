using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tableproj.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tableproj.Controller
{
    internal class ExcelController<T>
    {

        private readonly Excel.Application ex;

        

        public ExcelController(string path)
        {
            ex = new Excel.Application();
            ex.SheetsInNewWorkbook = 1;
            ex.Workbooks.Open(path);

        }

        public double[,] ReadExcel(int rowCount, int columnCount)
        {
            double[,] dataTable = new double[rowCount, columnCount];

            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            double t;

            for (int i = 0; i < rowCount; i++) 
            {
                for(int j = 0; j < columnCount; j++)
                {
                    double.TryParse(sheet.Cells[i + 2, j + 2].Value2.ToString(),out t);
                    dataTable[i,j] = t;
                }
            }
            return dataTable;
        }
        public void SaveExcel(string path) 
        {
            ex.Workbooks[1].SaveAs(path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Tableproj.Model
{
    internal class ExcelClass
    {
        public Application ex { get; set; }

        public ExcelClass()
        {
            ex = new Application();
            ex.Workbooks.Add(Type.Missing);
            ex.Workbooks[1].Activate();
            ex.Sheets.Add(Type.Missing);
            ((Worksheet)this.ex.ActiveWorkbook.Sheets[1]).Select();
        }

        public ExcelClass(string path)
        {
            ex = new Application();
            ex.Workbooks.Open(path);
            ex.Workbooks[1].Activate();
            ((Worksheet)this.ex.ActiveWorkbook.Sheets[1]).Select();
        }

        public void AddSheet(string name)
        {
            ex.Sheets.Add(name);
        }

        public Worksheet GetWorksheet(string name) => ex.Sheets[name];

        public void RemoveSheet(string name) => ex.Sheets[name].Delete();

        public void SetColumnName(string name, int col)
        {
            ex.Sheets[1].Cells[1, col].Value = name;
        }

        public void SetRowName(string name, int row)
        {
            ex.Sheets[1].Cells[row, 1].Value = name;
        }

        //public void AutoF()
        //{
        //    Worksheet sheet = ex.Worksheets.Item[1];
        //    ((Worksheet)sheet.Columns[1]).
        //}

        public void Show()
        {
            ex.Visible = true;
        }

        public void SaveAs(string path)
        {
            ex.Workbooks[1].SaveAs2(path);
        }

        public void FillRow(string data, int row)
        {
            string[] subd = data.Split(' ');
            for (int i = 0; i < subd.Length; i++)
            {
                ex.Cells[row, i + 2].Value = subd[i];
            }
        }

        public void SwitchRows(int row1, int row2)
        {
            Worksheet sheet = ex.Sheets[1];
            for (int i = 2; i < 12; i++)
            {
                double buffer = sheet.Cells[row1, i].Value;
                sheet.Cells[row1, i].Value = sheet.Cells[row2, i].Value;
                sheet.Cells[row2, i].Value = buffer;
            }         
        }
    }
}

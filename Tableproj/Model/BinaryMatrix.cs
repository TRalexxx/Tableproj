using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{


    internal class BinaryMatrix : IMatrix
    {
        public bool[,] DataMatrix { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public BinaryMatrix(int rows, int columns)
        {
            DataMatrix = new bool[rows, columns];
            Rows = rows;
            Columns = columns;
        }

        

        public void FillMartixRandom()
        {
            Random random = new Random();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    DataMatrix[i, j] = random.Next(2) == 1;
                }
            }

        } 

        public double[] GetRow(int row)
        {
            double[] row1 = new double[Columns];
            for (int i = 0; i < Columns; i++)
            {
                if (DataMatrix[row, i]) row1[i] = 1;
                else row1[i] = 0;                
            }
            return row1;
        }

        public int GetRowCount() => Rows;

        public int GetColumnCount() => Columns;


        public bool[,] GetBinaryMatrix() => DataMatrix;

        public double[,] GetMatrix()
        {
            throw new NotImplementedException();
        }

        public double[,] GetDoubleMatrix()
        {
            throw new NotImplementedException();
        }


    }
}

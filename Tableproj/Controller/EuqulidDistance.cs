using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tableproj.Model;

namespace Tableproj.Controller
{
    internal class EuclidDistance : IDistance, IMatrix
    {
        public double[,] DistanceMatrix { get; set; }

        public int Rows { get; set; }

        public EuclidDistance(int rows)
        {
            DistanceMatrix = new double[rows, rows];
            Rows = rows; 

        }

        public double Distance(double[] row1, double[] row2, int columns)
        {
            double sum = 0;
            for (int i = 0; i < columns; i++)
            {
                sum += Math.Pow((row1[i] - row2[i]), 2);
            }
            return Math.Round(Math.Sqrt(sum), 2);
        }

        public void CalculateBinaryMatrixDistance(IMatrix matrix)
        {
            for (int i = 0; i < matrix.GetRowCount(); i++)
            {
                for (int j = 0; j < matrix.GetRowCount(); j++)
                {
                    DistanceMatrix[i, j] = Distance(matrix.GetRow(i), matrix.GetRow(j), matrix.GetColumnCount());
                }
            }            
        }     
        
        public int GetRowCount() => Rows;

        public int GetColumnCount() => Rows;

        public double[,] GetDoubleMatrix() => DistanceMatrix;

        public bool[,] GetBinaryMatrix()
        {
            throw new NotImplementedException();
        }
        public double[] GetRow(int row)
        {
            throw new NotImplementedException();
        }
    }
}

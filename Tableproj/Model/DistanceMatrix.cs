using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{
    internal class DistanceMatrix
    {
        public int Size { get; }

        private readonly double[,] Matrix;
        
        public DistanceMatrix(DataTable<double> dataTable, IDistance distance)
        {
            Size = dataTable.RowCount;
            Matrix = new double[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Matrix[i, j] = distance.GetDistance(dataTable.GetRow(i), dataTable.GetRow(j));
                }
            }
        }

        public double this[int row, int column]
        {
            get => Matrix[row, column];
        }
    }
}

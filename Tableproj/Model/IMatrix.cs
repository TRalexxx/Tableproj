using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{
    internal interface IMatrix
    {
        public double[,] GetDoubleMatrix();

        public bool[,] GetBinaryMatrix();
        public double[] GetRow(int row);

        public int GetRowCount();

        public int GetColumnCount();
    }
}

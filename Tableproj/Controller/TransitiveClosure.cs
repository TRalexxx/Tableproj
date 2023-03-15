using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tableproj.Model;

namespace Tableproj.Controller
{
    internal class TransitiveClosure : IMatrix
    {

        public bool[,] ClosureMatrix { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public TransitiveClosure(IMatrix matrix, double criticalDistance)
        {
            Rows = matrix.GetRowCount();
            Columns = matrix.GetColumnCount();

            ClosureMatrix = new bool[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    if (matrix.GetDoubleMatrix()[i, j] <= criticalDistance)
                    {
                        ClosureMatrix[i, j] = true;
                    }
                    else { ClosureMatrix[i, j] = false; }
                }
            }
        }

        public void CheckTransitiveClosure()
        {
            int i, j, k;

            int flag = 1;

            do
            {
                flag = 1;
                for (k = 1; k < Rows; k++)
                {
                    for (i = 1; i < Rows; i++)
                    {
                        for (j = 1; j < Rows; j++)
                        {
                            if (ClosureMatrix[i, k] && ClosureMatrix[k, j])
                            {
                                flag = -1;
                                ClosureMatrix[i, j] = true;
                            }
                        }
                    }
                }
            } while (flag<0);
        }

        public int GetColumnCount() => Columns;

        public double[,] GetMatrix()
        {
            throw new NotImplementedException();
        }

        public double[] GetRow(int row)
        {
            throw new NotImplementedException();
        }

        public int GetRowCount() => Rows;


        public double[,] GetDoubleMatrix()
        {
            throw new NotImplementedException();
        }

        public bool[,] GetBinaryMatrix() => ClosureMatrix;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{
    internal class DataTable<T>
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        private readonly T[] data;

        public DataTable(int rowCount, int columnCount)
        {
            if (rowCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rowCount));
            }
            if (columnCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnCount));
            }
            RowCount = rowCount;
            ColumnCount = columnCount;
            data = new T[rowCount * columnCount];
        }

        public DataTable(T[,] array): this(array.GetLength(0), array.GetLength(1)) 
        { 
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = array[i, j];
                }
            }
        }



        public ref T this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= RowCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(row));
                }
                if (column < 0 || column >= RowCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(row));
                }

                return ref data[row * ColumnCount + column];
            }
        }

        public Span<T> GetRow(int row)
        {
            if (row < 0 || row >= RowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            return new Span<T>(data, row * ColumnCount, ColumnCount);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    sb.Append(this[i, j].ToString());
                    sb.Append("\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
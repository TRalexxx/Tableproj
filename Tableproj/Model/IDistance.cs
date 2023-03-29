using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tableproj.Model
{
    internal interface IDistance
    {
        public double GetDistance(Span<double> row1, Span<double> row2);
    }
    
    internal class EuclideanDistance : IDistance
    {
        public double GetDistance(Span<double> row1, Span<double> row2)
        {
            if (row2.Length != row1.Length)
            {
                throw new ArgumentException(nameof(row2));
            }

            double sum = 0;

            for (int i = 0; i < row1.Length; i++)
            {
                double difference = row1[i] - row2[i];
                sum += difference * difference;
            }

            return Math.Sqrt(sum);
        }
    }
}
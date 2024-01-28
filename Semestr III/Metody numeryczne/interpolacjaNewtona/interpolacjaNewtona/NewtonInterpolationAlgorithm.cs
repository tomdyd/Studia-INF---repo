using interpolacjaNewtona.Interfaces;

namespace interpolacjaNewtona
{
    public class NewtonInterpolationAlgorithm : INewtonInterpolationAlgorithm
    {
        public List<double> NewtonInterpolation(double[] x, double[] y, double a)
        {
            int n = x.Length - 1;
            double[,] d = new double[n, n];

            var list = new List<double>();
            var list1 = new List<double>();
            var temp = new List<double>();

            double[] horner = new double[n];

            for (int i = 0; i < 1; i++)
            {
                for (int j = i; j < n; j++)
                {
                    d[i, j] = (y[j + 1] - y[j]) / (x[j + 1] - x[j]); // (y1 - y0)/(x1 - x0)
                }
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    d[i, j - i] = (d[i - 1, j - i + 1] - d[i - 1, j - i]) / (x[j + 1] - x[j - i]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                list.Add(d[i, 0]);
            }
            list.Reverse();
            list.Add(y[0]); //pierwszy węzeł

            for (int i = 0; i < n; i++)
            {
                horner[i] = a - x[i];
            }

            var test = horner.Reverse().ToList();

            list1 = list;

            for (int i = 0; i <= list.Count - 2; i++)
            {
                temp.Add(list1[i] * test[i]);
                list1[i + 1] = list[i + 1] + temp[i];
            }

            return list1;
        }
    }
}

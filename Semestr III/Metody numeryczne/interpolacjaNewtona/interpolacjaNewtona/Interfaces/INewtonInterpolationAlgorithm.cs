using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpolacjaNewtona.Interfaces
{
    public interface INewtonInterpolationAlgorithm
    {
        List<double> NewtonInterpolation(double[] x, double[] y, double a);
    }
}

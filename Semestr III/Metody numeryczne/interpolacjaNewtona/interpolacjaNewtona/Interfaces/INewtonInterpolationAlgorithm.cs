namespace interpolacjaNewtona.Interfaces
{
    public interface INewtonInterpolationAlgorithm
    {
        List<double> NewtonInterpolation(double[] x, double[] y, double a);
    }
}

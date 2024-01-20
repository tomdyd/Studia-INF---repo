using interpolacjaNewtona;
using interpolacjaNewtona.Interfaces;

class Program
{
    static void Main()
    {
        INewtonInterpolationAlgorithm newtonInterpolationAlgorithm = new NewtonInterpolationAlgorithm();
        IAppConsole appConsole = new AppConsole();
        IAppRunner appRunner = new AppRunner(newtonInterpolationAlgorithm, appConsole);

        appRunner.startApp();
    }

    
}

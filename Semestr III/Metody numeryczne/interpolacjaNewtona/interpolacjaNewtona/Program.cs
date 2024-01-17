using interpolacjaNewtona;
using interpolacjaNewtona.Interfaces;
using System;
using System.Runtime.ExceptionServices;

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

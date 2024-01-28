using interpolacjaNewtona.Interfaces;

namespace interpolacjaNewtona
{
    public class AppRunner : IAppRunner
    {
        private readonly INewtonInterpolationAlgorithm _newtonInterpolationAlgorithm;
        private readonly IAppConsole _appConsole;

        public AppRunner(INewtonInterpolationAlgorithm newtonInterpolationAlgorithm, IAppConsole appConsole)
        {
            _newtonInterpolationAlgorithm = newtonInterpolationAlgorithm;
            _appConsole = appConsole;
        }

        public void startApp()
        {
            while (true)
            {
                _appConsole.Clear();
                _appConsole.Siganture();
                var res = _appConsole.getIntFromUser("1. Start\n2. Wyjdź\n");

                _appConsole.Clear();
                _appConsole.Siganture();

                switch (res)
                {
                    case 1:
                        res = _appConsole.getIntFromUser("Podaj liczbę węzłów interpolacji: ");

                        double[] x = new double[res];
                        double[] y = new double[res];

                        for (int i = 0; i < res; i++)
                        {
                            x[i] = _appConsole.getDoubleFromUser($"Podaj {i + 1} węzeł interpolacji: ");
                        }

                        _appConsole.WriteLine("--------------------------------");

                        for (int i = 0; i < res; i++)
                        {
                            y[i] = _appConsole.getDoubleFromUser($"Podaj {i + 1} wartość: ");
                        }

                        _appConsole.WriteLine("--------------------------------");

                        double a = _appConsole.getDoubleFromUser("Podaj dla jakiego x mam obliczyć interpolację: ");

                        var result = _newtonInterpolationAlgorithm.NewtonInterpolation(x, y, a);

                        _appConsole.WriteLine("--------------------------------");

                        _appConsole.WriteLine($"Wartość wielomianu interpolacyjnego dla x = {a} wynosi: {Math.Round(result.Last(), 4)}");

                        _appConsole.WriteLine("Kliknij przycisk aby kontynuować...");
                        _appConsole.ReadLine();
                        break;

                    case 2:
                        return;
                }
            }
        }
    }
}

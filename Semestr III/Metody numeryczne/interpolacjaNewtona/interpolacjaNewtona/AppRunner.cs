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
                Siganture();
                _appConsole.WriteLine("1. Kontynuuj");
                _appConsole.WriteLine("2. Wyjdź");
                var res = int.Parse(_appConsole.ReadLine());

                switch (res)
                {
                    case 1:
                        _appConsole.Write("Podaj liczbę węzłów interpolacji: ");
                        res = int.Parse(_appConsole.ReadLine());

                        double[] x = new double[res];
                        double[] y = new double[res];

                        for (int i = 0; i < res; i++)
                        {
                            _appConsole.Write($"Podaj {i + 1} węzeł interpolacji: ");
                            x[i] = double.Parse(_appConsole.ReadLine());
                        }

                        _appConsole.WriteLine("--------------------------------");

                        for (int i = 0; i < res; i++)
                        {
                            _appConsole.Write($"Podaj {i + 1} wartość: ");
                            y[i] = double.Parse(_appConsole.ReadLine());
                        }

                        _appConsole.WriteLine("--------------------------------");

                        _appConsole.Write("Podaj dla jakiego x mam obliczyć interpolację: ");
                        double a = double.Parse(_appConsole.ReadLine());

                        var result = _newtonInterpolationAlgorithm.NewtonInterpolation(x, y, a);

                        _appConsole.WriteLine("--------------------------------");

                        _appConsole.WriteLine($"Wartość wielomianu interpolacyjnego dla x = {a} wynosi: {result.Last()}");

                        _appConsole.WriteLine("Kliknij przycisk aby kontynuować...");
                        _appConsole.ReadLine();
                        break;

                    case 2:
                        return;
                }
            }
        }

        private void Siganture()
        {
            _appConsole.WriteLine(".______    __  .__   __.      ___      .______     ____    ____  _______  ____    ____  _______       ___      \r\n|   _  \\  |  | |  \\ |  |     /   \\     |   _  \\    \\   \\  /   / |       \\ \\   \\  /   / |       \\     /   \\     \r\n|  |_)  | |  | |   \\|  |    /  ^  \\    |  |_)  |    \\   \\/   /  |  .--.  | \\   \\/   /  |  .--.  |   /  ^  \\    \r\n|   _  <  |  | |  . `  |   /  /_\\  \\   |      /      \\_    _/   |  |  |  |  \\_    _/   |  |  |  |  /  /_\\  \\   \r\n|  |_)  | |  | |  |\\   |  /  _____  \\  |  |\\  \\----.   |  |     |  '--'  |    |  |     |  '--'  | /  _____  \\  \r\n|______/  |__| |__| \\__| /__/     \\__\\ | _| `._____|   |__|     |_______/     |__|     |_______/ /__/     \\__\\ \r\n                                                                                                               ");
        }
    }
}

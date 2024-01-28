namespace interpolacjaNewtona
{
    public class Program
    {
        static void Main()
        {
            INewtonInterpolationAlgorithm newtonInterpolationAlgorithm = new NewtonInterpolationAlgorithm();
            IAppConsole appConsole = new AppConsole();
            IAppRunner appRunner = new AppRunner(newtonInterpolationAlgorithm, appConsole);

            appRunner.startApp();
        }
    }
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
    public class AppConsole : IAppConsole
    {
        public void Clear()
        {
            Console.Clear();
        }

        public string ReadLine()
        {
            var res = Console.ReadLine();
            return res;
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public ConsoleKeyInfo ReadKey()
        {
            var key = Console.ReadKey();
            return key;
        }

        public int getIntFromUser(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                var res = int.TryParse(Console.ReadLine(), out var val);

                if (res)
                {
                    return val;
                }
                else
                {
                    Console.Clear();
                    Siganture();
                    Console.WriteLine("Musisz podać liczbę!");
                }

            }
        }

        public double getDoubleFromUser(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                var res = double.TryParse(Console.ReadLine(), out var val);

                if (res)
                {
                    return val;
                }
                else
                {
                    Console.Clear();
                    Siganture();
                    Console.WriteLine("Musisz podać liczbę!");
                }

            }
        }

        public void Siganture()
        {
            {
                Console.WriteLine(".______    __  .__   __.      ___      .______     ____    ____  _______  ____    ____  _______       ___      \r\n|   _  \\  |  | |  \\ |  |     /   \\     |   _  \\    \\   \\  /   / |       \\ \\   \\  /   / |       \\     /   \\     \r\n|  |_)  | |  | |   \\|  |    /  ^  \\    |  |_)  |    \\   \\/   /  |  .--.  | \\   \\/   /  |  .--.  |   /  ^  \\    \r\n|   _  <  |  | |  . `  |   /  /_\\  \\   |      /      \\_    _/   |  |  |  |  \\_    _/   |  |  |  |  /  /_\\  \\   \r\n|  |_)  | |  | |  |\\   |  /  _____  \\  |  |\\  \\----.   |  |     |  '--'  |    |  |     |  '--'  | /  _____  \\  \r\n|______/  |__| |__| \\__| /__/     \\__\\ | _| `._____|   |__|     |_______/     |__|     |_______/ /__/     \\__\\ \r\n                                                                                                               ");
            }
        }
    }
    public interface INewtonInterpolationAlgorithm
    {
        List<double> NewtonInterpolation(double[] x, double[] y, double a);
    }
    public interface IAppRunner
    {
        void startApp();
    }
    public interface IAppConsole
    {
        void WriteLine(string message);
        void Write(string message);
        string ReadLine();
        ConsoleKeyInfo ReadKey();
        void Clear();
        int getIntFromUser(string msg);
        double getDoubleFromUser(string msg);
        void Siganture();

    }

}

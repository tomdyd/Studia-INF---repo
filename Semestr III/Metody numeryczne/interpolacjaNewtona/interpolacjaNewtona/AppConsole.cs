using interpolacjaNewtona.Interfaces;

namespace interpolacjaNewtona
{
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
            while(true)
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
}

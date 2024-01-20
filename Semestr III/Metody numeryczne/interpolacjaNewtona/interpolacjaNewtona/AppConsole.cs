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
    }
}

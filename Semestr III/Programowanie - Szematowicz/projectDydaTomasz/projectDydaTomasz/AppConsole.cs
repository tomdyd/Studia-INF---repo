
using projectDydaTomasz.Interfaces;

namespace projectDydaTomasz
{
    public class AppConsole : IAppConsole
    {
        public int GetResponseFromUser()
        {
            while (true)
            { 
                var res = Console.ReadLine();

                if (int.TryParse(res, out var intResponse))
                {
                    return intResponse;
                }
                Console.WriteLine("Jeszcze raz - to nie jest int");
            }
        }

        public string GetDataFromUser(string msg)
        {
            Console.Write(msg);
            var res = Console.ReadLine();
            return res;
        }

        public string GetLoginFromUser()
        {
            Console.Write("Podaj login: ");
            var response = Console.ReadLine();
            return response;
        }

        public string GetPasswordFromUser()
        {
            Console.Write("Podaj hasło: ");
            var response = Console.ReadLine();
            return response;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(object msg)
        {
            Console.WriteLine(msg);
        }

        public void Write(object msg)
        {
            Console.Write(msg);
        }
    }
}

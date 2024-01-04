
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
    }
}

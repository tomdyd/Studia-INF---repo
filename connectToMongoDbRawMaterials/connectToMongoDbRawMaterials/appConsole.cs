using connectToMongoDbRawMaterials.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectToMongoDbRawMaterials
{
    public class appConsole : IAppConsole
    {
        public void Clear()
        {
            Console.Clear();
        }

        public int GetResponeFromUser()
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
        public string GetDataFromUser(string message)
        {
            Console.Write(message);
            var res = Console.ReadLine();
            return res;
        }

        public string ReadLine()
        {
            var res = Console.ReadLine();
            return res;
        }

        public void Write(string messsage)
        {
            Console.WriteLine(messsage);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}

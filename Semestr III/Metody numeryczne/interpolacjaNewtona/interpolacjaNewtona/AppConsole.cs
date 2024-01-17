using interpolacjaNewtona.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using usbEventDetector.Interfaces;

namespace usbEventDetector
{
    internal class MyConsole : IConsole
    {
        public void ConsoleClear()
        {
            Console.Clear();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}

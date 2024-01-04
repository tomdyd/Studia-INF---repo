using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace pierwiastekKwadratowy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj p: ");
            double p = double.Parse(Console.ReadLine());
            Console.Write("Podaj a: "); //to jest liczba pierwiastkowana
            int a = int.Parse(Console.ReadLine());
            Console.Write("Podaj e: ");
            double e = double.Parse(Console.ReadLine());
            double temp;
            do
            {
                double x = p;
                p = (x + (a / x))/2;
                temp = Math.Abs(p - x);
            } while (temp > e);

            Console.WriteLine(p);
            Console.ReadKey();
        }
    }
}

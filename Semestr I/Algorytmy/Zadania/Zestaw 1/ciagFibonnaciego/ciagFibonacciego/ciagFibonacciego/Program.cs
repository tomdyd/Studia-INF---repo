using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciagFibonacciego
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj ile wyrazów ciągu mam wypisać: ");
            bool czyLiczba;
            czyLiczba = int.TryParse(Console.ReadLine(), out int number);

            do
            {
                if (!czyLiczba)
                {
                    Console.Write("Podaj liczbe: ");
                    czyLiczba = int.TryParse(Console.ReadLine(), out number);
                }

            } while (czyLiczba != true);

            double[] Fib;
            Fib = new double[100];

            for (int i = 0; i <= number - 1; i++)
            {
                if (i == 0 || i == 1)
                {
                    Fib[i] = 1;
                    Console.WriteLine($"{i + 1}. {Fib[i]}");
                }

                else
                {
                    Fib[i] = Fib[i - 1] + Fib[i - 2];
                    Console.WriteLine($"{i + 1}. {Fib[i]}");
                }
            }


            Console.ReadLine();
        }
    }
}

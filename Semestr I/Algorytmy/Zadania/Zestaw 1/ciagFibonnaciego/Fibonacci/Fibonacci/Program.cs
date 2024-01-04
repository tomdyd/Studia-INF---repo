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
            ConsoleKey key;
            do
            {
                Console.Clear();
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
                Fib = new double[number];

                if (number == 0)
                {
                    Console.WriteLine("Wyraz zerowy ciągu nie istnieje!");
                    Console.WriteLine("\n Aby opuścić program wciśnij ESC, aby kontynuować wciśnij ENTER.");
                    key = Console.ReadKey().Key;

                    while (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
                    {
                        if (key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("\n Wciśnij ENTER lub ESC");
                            key = Console.ReadKey().Key;
                        }
                    }
                }

                else
                {
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

                    Console.WriteLine("\n Aby opuścić program wciśnij ESC, aby kontynuować wciśnij ENTER.");
                    key = Console.ReadKey().Key;

                    while (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
                    {
                        if (key != ConsoleKey.Enter)
                        {
                            Console.WriteLine("\n Wciśnij ENTER lub ESC");
                            key = Console.ReadKey().Key;
                        }
                    }
                }

            } while (key != ConsoleKey.Escape);
        }  

    }
}
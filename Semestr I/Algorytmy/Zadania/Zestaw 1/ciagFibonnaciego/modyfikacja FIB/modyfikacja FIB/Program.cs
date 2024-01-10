﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                bool isNumber;
                isNumber = int.TryParse(Console.ReadLine(), out int number);

                    if (!isNumber)
                    {
                        do
                        {
                            Console.Clear();
                            Console.Write("Podaj liczbe: ");
                            isNumber = int.TryParse(Console.ReadLine(), out number);
                        } while (!isNumber);
                    }


                while (number <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Podaj liczbę która jest dodatnia!");
                    Console.Write("Podaj ile wyrazów ciągu mam wypisać: ");
                    isNumber = int.TryParse(Console.ReadLine(), out number);
                }

                if (number > 1476)
                {
                    Console.WriteLine("INFINITY");
                    key = wantToStop();
                }

                else
                {
                    double[] Fib;
                    Fib = new double[number];

                    double fi;

                    for (int i = 0; i < number; i++)
                    {
                        if (i == 0 || i == 1)
                        {
                            Fib[i] = 1;
                            Console.WriteLine($"{i + 1}. {Fib[i]}");

                            if(number == 1)
                                Console.WriteLine("Nie można podać złotej liczby dla jednego wyrazu ciągu");

                            else if(i == 1 && number == 2) //jeśli warunkiem byłoby number == 2 to zostałoby to wypisane 2 razy, ponieważ jesteśmy w pętli i zostanie to wypisane dla każdej zmiennej i
                                Console.WriteLine("Złota liczba dla 2 wyrazu ciągu: 1");
                        }

                        else
                        {
                            Fib[i] = Fib[i - 1] + Fib[i - 2];
                            Console.WriteLine($"{i + 1}. {Fib[i]}");

                            if (i == number - 1)
                            {
                                fi = Fib[i] / Fib[i - 1];
                                Console.WriteLine($"\n Złota liczba dla {i+1} wyrazu ciągu: {fi}.");
                            }
                        }
                    }
                    key = wantToStop();
                }
            } while (key != ConsoleKey.Escape);
        }

        static ConsoleKey wantToStop()
        {
            ConsoleKey key;
            Console.WriteLine("\n Aby opuścić program wciśnij ESC, aby kontynuować wciśnij ENTER.");
            key = Console.ReadKey().Key;

            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
            {
                    Console.Clear();
                    Console.WriteLine("\n Aby opuścić program wciśnij ESC, aby kontynuować wciśnij ENTER.");
                    key = Console.ReadKey().Key;
            }
            return key;
        }
    }
}

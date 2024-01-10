﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace WitajSwiecie
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, result, result1, delta, sqrtdelta, x, x1, x2;
            ConsoleKey letter, letter1;
            do
            {
                letter1 = ConsoleKey.N;
                Console.Clear();

                Console.WriteLine("Informacja dla użytkownika!\nDo programu należy wprowadzić 4 zmienne. " +
                    "\nZmienna a - liczba znajdująca się przy x^2\nzmienna b - znajdująca się przy x\nzmienna c - liczba w równaniu kwadratowym" +
                    "\nzmienna d - wynik równania\nRównanie należy wprowadzić w takiej postaci, aby wszystkie niewiadome znajdowały się po tej samej stronie!\n");
                Console.Write("Podaj zmienną a: ");
                a = readNumber();
                Console.Write("Podaj zmienną b: ");
                b = readNumber();
                Console.Write("Podaj zmienną c: ");
                c = readNumber();
                Console.Write("Podaj zmienną d: ");
                result = readNumber();

                c = c - result; //przeniesienie liczby z prawej strony równania na lewą stronę równania
                result1 = result;
                result = 0;

                if (a != 0 && b != 0 && c!= 0) // ax^2 + bx + c = 0
                {            
                    Console.Write($"\nCzy równanie ma postać: {a}x^2 + {b}x + {c} = {result} ?");

                    letter = yesOrNo();
                    if (letter == ConsoleKey.N)
                        continue;
                    
                    delta = b * b - (4 * a * c);

                    if (delta > 0)
                    {
                        sqrtdelta = Math.Sqrt(delta);
                        x1 = (-b - sqrtdelta) / (2 * a);
                        x2 = (-b + sqrtdelta) / (2 * a);
                        Console.WriteLine($"\n\nPierwiastek z delty: {sqrtdelta} \nx1 = {x1} \nx2 = {x2}");
                    }

                    else if (delta == 0)
                    {
                        x = -b / (2 * a);
                        Console.WriteLine($"\n\nDelta: {delta} \nx = {x}");
                    }

                    else //LICZBY ZESPOLONE
                    {
                        Console.WriteLine("\n\nRozwiązaniem tego równania są liczby zespolone, sqrt(x) oznacza pierwiastek z liczby x");
                        delta = -delta; //zamiana ujemnej delty na delte dodatnia

                        Console.WriteLine($"\n        {b}     sqrt({delta})i");
                        Console.WriteLine($"x1 = - ---- - ---------");
                        Console.WriteLine($"        {2*a}        {2*a}");

                        Console.WriteLine($"\n        {b}     sqrt({delta})i");
                        Console.WriteLine($"x2 = - ---- + ---------");
                        Console.WriteLine($"        {2 * a}        {2 * a}");

                        Console.ReadLine();
                    }
                }
                else if (a == 0 && b == 0) // c = wynik
                {
                    Console.WriteLine("To nie jest równanie.");
                    Console.WriteLine("Naciśnij dowolony przycisk aby kontynuować.");
                    letter = Console.ReadKey().Key;
                }

                else if (a == 0 && c == 0) // bx = 0, wynik równy 0, patrz linijka 29
                {
                    Console.Write($"\nCzy równanie ma postać: {b}x = {result} ?");

                    letter = yesOrNo();
                    if (letter == ConsoleKey.N)
                        continue;

                    Console.WriteLine("\n\nx = 0");
                }

                else if (a == 0) // bx = -c, wynik równy 0, patrz linijka 29
                {
                    c = c + result1;
                    result1 = result1 - c;
                    Console.Write($"\nCzy równanie ma postać: {b}x = {result1} ?");

                    letter = yesOrNo();
                    if (letter == ConsoleKey.N)
                        continue;

                    x = result1 / b;
                    Console.WriteLine($"\n\nx = {x}");
                }

                else if (b == 0) // ax^2 = result1, ponieważ po prawej stonie musi być liczba aby obliczyć to równanie
                {
                    c = c + result1; //przywrócenie pierwotnej wartości c (patrz linijka 27)
                    result1 = result1 - c;
                    Console.Write($"\nCzy równanie ma postać: {a}x^2 = {result1} ?");

                    letter = yesOrNo();
                    if (letter == ConsoleKey.N)
                        continue;

                    if (result1 > 0)
                    {
                        x1 = Math.Sqrt(result1);
                        Console.WriteLine($"\n\nx1 = {x1}\nx2 = {-x1}");
                    }
                    else
                        Console.WriteLine("\n\nBłąd, żadna liczba podniesiona do kwadratu nie da wyniku ujemnego lub równego 0");
                }

                else // ax^2 +bx = 0, jest to dalej równanie kwadratowe
                {
                    Console.Write($"\nCzy równanie ma postać: {a}x^2 + {b}x = {result} ?");

                    letter = yesOrNo();
                    if (letter == ConsoleKey.N)
                        continue;

                    delta = b * b;

                    if (delta > 0)
                    {
                        sqrtdelta = Math.Sqrt(delta);
                        x1 = (-b - sqrtdelta) / (2 * a);
                        x2 = (-b + sqrtdelta) / (2 * a);
                        Console.WriteLine($"\n\nPierwiastek z delty: {sqrtdelta} \nx1 = {x1}\nx2 = {x2}");
                    }
                }
                Console.Write("\nCzy wyjść z programu?\nY/N");
                letter1 = Console.ReadKey().Key;

            } while (letter1 != ConsoleKey.Y);
        }

        static double readNumber()
        {
            double result;
            bool isNumber;

                isNumber = double.TryParse(Console.ReadLine(), out result);
            while(!isNumber)
            {
                Console.Write("Podaj liczbę! ");
                isNumber = double.TryParse(Console.ReadLine(), out result);
            }

            return result;
        }

        static ConsoleKey yesOrNo()
        {
            ConsoleKey letter;
            Console.Write("\nY/N");
            letter = Console.ReadKey().Key;
            while (letter != ConsoleKey.Y && letter != ConsoleKey.N)
            {
                Console.WriteLine("\nY/N");
                letter = Console.ReadKey().Key;
            }
            return letter;
        }
    }
}

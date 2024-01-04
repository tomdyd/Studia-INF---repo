﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Trojkaty
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
            double[] a;
            a = new double[3];

            for (int i = 0; i < 3; i++)
            {
                a[i] = liczba($"Podaj długość boku {i + 1}: ");
            }

            double suma = 0;

                if (a[0] >= a[1] + a[2] || a[1] >= a[0] + a[2] || a[2] >= a[0] + a[1]) //warunek istnienia trójkąta
                {
                    for (int i = 0; i < 3; i++)
                    {
                        double temp = a[i];
                        a[i] = 0; //jeżeli mamy 3 wartosci i jedną z nich wyzerujemy a następnie wszystkie dodamy to otrzymamy sume 2 pozostałych wartości

                        suma = 0;

                        for (int k = 0; k < 3; k++)
                        {
                            suma = suma + a[k];
                        }

                        a[i] = temp;

                        if (a[i] > suma)
                        {
                            Console.WriteLine($"Bok {i + 1} jest większy od sumy pozostałych dwóch boków!");
                        }
                    }
                    Console.WriteLine("Taki trojkąt nie istnieje!");

                    Console.WriteLine($"\nBok a = {a[0]}, bok b = {a[1]}, bok c = {a[2]}");
                    Console.ReadKey();
                }
                else
                {
                    double S;
                    double p = (a[0] + a[1] + a[2]) / 2;

                    S = Math.Sqrt(p * (p - a[0]) * (p - a[1]) * (p - a[2])); //wzór Herona
                    Console.WriteLine($"Pole trójkąta wynosi: {S}");

                    for (int i = 0; i < 3; i++) //sortowanie w celu okreslenia najmniejszego boku
                    {
                        double temp = a[i];

                        for (int x = i; x < 3; x++)
                        {
                            if (a[x] < temp)
                            {
                                double temp1;

                                temp1 = a[i];
                                temp = a[x];
                                a[i] = a[x];
                                a[x] = temp1;
                            }
                        }
                        a[i] = temp;
                    }

                    double[] kwadrat;
                    kwadrat = new double[3];

                    for (int i = 0; i < 3; i++)
                    {
                        kwadrat[i] = a[i] * a[i];                   //długości boków trojkąta w tablicy mają tendencję rosnącą, dlatego wartość pod indeksem 3 jest największa
                    }

                    string rodzajTrojkata = "";

                    if (kwadrat[0] + kwadrat[1] < kwadrat[2])       //jeżeli suma pól dwóch kwadratów zbudowanych na krótszych bokach jest
                        rodzajTrojkata = "Trójkąt rozwartokątny";   //mniejsza od pola kwadratu zbudowanego na najdłuższym boku to trojkat jest rozwartokątny

                    else if (kwadrat[0] + kwadrat[1] == kwadrat[2]) //jeżeli suma pól dwóch kwadratów zbudowanych na krótszych bokach jest
                        rodzajTrojkata = "Trójkąt prostokątny";     //równa polu kwadratu zbudowanego na najdłuższym boku to trojkat jest prostokątny

                    else
                        rodzajTrojkata = "Trójkąt ostrokątny";      //jeżeli suma pól dwóch kwadratów zbudowanych na krótszych bokach jest
                                                                    //większa od pola kwadratu zbudowanego na najdłuższym boku to trojkat jest ostrokątny
                    if (a[0] == a[1] && a[1] == a[2])
                        rodzajTrojkata = rodzajTrojkata + " równoboczny";

                    else if (a[0] == a[1] || a[1] == a[2])
                        rodzajTrojkata = rodzajTrojkata + " równoramienny";

                    else
                        rodzajTrojkata = rodzajTrojkata + " różnoboczny";

                    Console.WriteLine(rodzajTrojkata);

                    Console.WriteLine("Nciśnij ESC aby wyjść z programu lub dowolny klawisz aby kontynuwać.");
                }
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static double liczba (string message)
        {
            bool czyLiczba;
            double x;

            Console.Write(message);
            czyLiczba = double.TryParse(Console.ReadLine(), out x);

            while (!czyLiczba)
            {
                    Console.Clear();
                    Console.WriteLine("Podaj liczbę!");
                    czyLiczba = double.TryParse(Console.ReadLine(), out x);
            }
            return x;
        }

    }
}
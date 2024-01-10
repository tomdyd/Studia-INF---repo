﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace algorytm_na_usuwanie_zer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj ilość elementów w zbiorze: ");
            bool czyLiczba = int.TryParse(Console.ReadLine(), out int i);
            while (!czyLiczba || i <= 0)
            {
                Console.WriteLine("Podana wartość musi być liczbą większą od 0");
                czyLiczba = int.TryParse(Console.ReadLine(), out i);
            }

            int howMany = i;
            int[] a = new int[i];

            for (int j = 0; j < i; j++)
            {
                Console.Write($"Podaj liczbę nr {j+1}: ");
                czyLiczba = int.TryParse(Console.ReadLine(), out int k);
                while(!czyLiczba)
                {
                    Console.WriteLine("Podana wartość musi być liczbą!");
                    czyLiczba = int.TryParse(Console.ReadLine(), out k);
                }
                a[j] = k;
            }

            int ile = 0;
            i = 0;

            while (i < howMany-ile)
            {
                if (a[i] == 0)
                {
                    ile++;
                    int k = i;
                    do
                    {
                        a[k] = a[k + 1];
                        ++k;
                    } while (k != howMany-1);
                }
                else
                {
                    i++;
                }
            }
            for (i = 0; i < howMany - ile; i++)
            {
                Console.WriteLine(a[i]);
            }
            Console.ReadLine();
        }
    }
}

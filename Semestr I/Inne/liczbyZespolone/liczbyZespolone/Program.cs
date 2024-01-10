using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace liczbyZespolone
{
    class Program
    {
        static void Main(string[] args)
        {
            bool czyLiczba;

            Console.Write("Podaj liczbę: ");
            czyLiczba = double.TryParse(Console.ReadLine(), out double liczba);

            while (!czyLiczba)
            {
                if (!czyLiczba)
                {
                    Console.Clear();
                    Console.WriteLine("Podaj liczbę: ");
                    czyLiczba = double.TryParse(Console.ReadLine(), out liczba);
                }
            }

            double pierwiastek;

            if (liczba < 0)
            {
                liczba = liczba * -1;

                pierwiastek = Math.Sqrt(liczba);
                Console.WriteLine($"Pierwiastek z {liczba} = i * {pierwiastek}");
            }
            else
            {
                pierwiastek = Math.Sqrt(liczba);
                Console.WriteLine($"Pierwiastek z {liczba} = {pierwiastek}");
            }
            Console.ReadKey();
        }
    }
}
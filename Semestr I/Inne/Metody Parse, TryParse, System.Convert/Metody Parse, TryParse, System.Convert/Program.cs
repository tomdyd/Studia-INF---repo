using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PromieńKoła
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputExit = "";
            string exit;
            do
            {
                float r = getRadius();
                double pi = 3.141592653589793;
                double circleField = pi * r * r;
                Console.WriteLine(circleField);


                    Console.WriteLine("Czy chcesz zakończyć program? true/false");
                    inputExit = Console.ReadLine();
                    exit = inputExit.ToLower();
                do
                {
                    if (exit != "true" && exit != "false")
                    {
                        Console.WriteLine("Podaj wartość true lub false");
                        exit = Console.ReadLine();
                    }
                    else if (exit == "false") continue;
                }
                while (exit != "true" && exit != "false");

            }
            while (exit != "true");

            Console.WriteLine("Naciśnij klawisz aby zamknąć okno");
            Console.ReadLine();
        }

        static float getRadius()
        {
            float radius;
            bool czyLiczba;
            do
            {
                Console.Write("Podaj promień: ");
                czyLiczba = float.TryParse(Console.ReadLine(), out radius);
                if (!czyLiczba)
                {
                    Console.WriteLine("Wprowadź liczbę!");
                    continue;
                }
            } while (czyLiczba != true);

            return radius;
        }
    }
}
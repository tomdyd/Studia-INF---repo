using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string exit = "";

            {
                do
                {
                    Console.WriteLine("Aby opuśić pętlę wpisz true, jeśli chcesz w niej pozostać wpisz false");
                    exit = Console.ReadLine();

                    if (exit != "true" && exit != "false")
                    {
                        Console.WriteLine("Wprowadź poprawną wartość true lub false");
                        exit = Console.ReadLine();
                    }

                    else if (exit == "false")
                    {
                        continue;
                    }

                } while (exit != "true");
            }

            Console.WriteLine("Jesteś poza pętlą");
            Console.ReadKey();
        }
    }
}
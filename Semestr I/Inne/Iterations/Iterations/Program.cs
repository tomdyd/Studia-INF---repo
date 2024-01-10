using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WitajSwiecie
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "";
            // do zmiennej userIsDone została przypisana wartość "tak", dlatego pętla while nie wykona się ani razu ponieważ warunek już na początku jest fałszywy
            string userIsDone = "tak";
            while (userIsDone != "tak")
            {
                Console.WriteLine("Program działa!");
                Console.WriteLine("Czy zakończyć działanie programu?");
                userInput = Console.ReadLine();
                userIsDone = userInput.ToLower();
            }

            Console.WriteLine("Jesteśmy za pętlą WHILE");
            Console.ReadKey();

            // do zmiennej userIsDone1 została przypisana wartość "tak", ale ponieważ została zastosowana
            // pętla DO WHILE to pętla wykona się 1raz, ponieważ warunek jest sprawdzany dopiero po jej wykonaniu.

            string userIsDone1 = "tak";
            do
            {
                Console.WriteLine("Program działa!");
                Console.WriteLine("Czy zakończyć działanie programu?");
                userIsDone1 = Console.ReadLine();
            }
            while (userIsDone1 != "tak");

            Console.WriteLine("Jestśmy za pętlą DO WHILE");
            Console.ReadKey();

        }
    }
}
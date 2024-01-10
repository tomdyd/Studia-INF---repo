using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
namespace WitajSwiecie
{
    class Program
    {
        static string profession = "Chemik";

        static void Main(string[] args)
        {
            Console.WriteLine($"Zmienna globalna w metodzie Main: {profession}");

            Console.WriteLine("Zmienne lokalne w metodzie Main");

            int age = 25;   //zadeklarowanie i inicjalizacja zmiennej 
            string name;     //zadeklarowanie zmiennej
            name = "Tomek"; //inicajlizacja zmiennej

            bool b1 = true, b2 = false, b3 = b2; //zadeklarowanie i inicajlizacja kilku zmiennych w jednej linii
            System.Boolean b4 = true;            //Użycie typu danych boolean do zadeklarowania wartości bool

            Console.WriteLine($"Imię {name}. Wiek {age}");
            string GreetingText = PrepareGreetingText("Tomek", 25); //Wywołanie metody PrepareGreetingText w metodzie Main
            Console.WriteLine(GreetingText);

            Console.WriteLine("Wywołanie zmiennej LocalVarDeclarations");
            LocalVarDeclarations(); //Wywołanie metody LocalVarDeclarations            

            Console.ReadKey();
        }

        static void LocalVarDeclarations()
        {
            Console.WriteLine($"Zmienna globalna w metodzie LocalVarDeclarations {profession}");

            Console.WriteLine("Zmienne lokalne w metodzie LocalVarDeclarations");
            int age = 30;
            int myInt = 4;
            double myDouble = 2.5;
            char myChar = 'a';
            string myString = "Hello!";

            Console.WriteLine($"{myInt} {myDouble} {myChar} {myString}");
            string GreetingText = PrepareGreetingText("Tomek", age); // Wywołanie metody PrepareGreetingText w metodzie LocalVarDeclarations
            Console.WriteLine(GreetingText);
        }

        static string PrepareGreetingText(string name, int age)
        {
            return $"Cześć {name}! Masz {age} lat.";
        }
    }
}
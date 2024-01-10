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
            //operacje na zmiennych string
            string FirstName = "Tomek";
            Console.WriteLine("Wartość zmiennej FirstName {0}", FirstName);
            Console.WriteLine("Napis w zmiennej FirstName ma {0} znaków.", FirstName.Length);
            Console.WriteLine("FirstName pisane dużymi literami: {0}", FirstName.ToUpper());
            Console.WriteLine("FirstName pisane małymi literami: {0}", FirstName.ToLower());
            Console.WriteLine("Czy napis w zmiennej FirstName zawiera literę T? {0}", FirstName.Contains("T"));
            Console.WriteLine("FirstName po zmianie: {0}", FirstName.Replace("ek", "aszek"));

            //konkatenacja na 2 sposoby
            string lastName = " Dyda";
            string s1 = FirstName + lastName; // Konkatenacja za pomocą znaku '+'. Formalnie kompilator wywołuje funkcję concat z klasy System, dlatego można to zapisać jak w lini 23
            Console.WriteLine(s1);
            string s2 = string.Concat(FirstName, lastName);
            Console.WriteLine(s2);


            //znaki ucieczki
            Console.WriteLine("Znaki ucieczki");
            string strWithTabs = "Marka\tkolor\tPrędkość\tNazwa";
            Console.WriteLine(strWithTabs);
            Console.WriteLine("Każdy napisał program wyświetlający napis \"Hello World!\"");
            Console.WriteLine("C:\\StringTypes\\bin\\Debug");
            Console.WriteLine("Koniec. \n\n");
            Console.WriteLine("Łańcuch dosłowny");
            Console.WriteLine(@"C:\StringTypes\bin\Debug");

            //porównywanie wartości zmiennych string
            string s3 = "Cześć";
            string s4 = "Elo";
            Console.WriteLine("s3 = {0}", s3);
            Console.WriteLine("s4 = {0}", s4);
            Console.WriteLine("s3 == s4: {0}", s3 == s4);
            Console.WriteLine("s3 == Cześć: {0}", s3 == "Cześć");
            Console.WriteLine("s3 == CZEŚĆ: {0}", s3 == "CZEŚĆ");
            Console.WriteLine("s3 == cześć: {0}", s3 == "cześć");
            Console.WriteLine("s3.Equals(s4) {0}", s3.Equals(s4));
            Console.WriteLine("Elo.Equals(s4) {0}", "Elo".Equals(s4));

            string s5 = "Tomek"; //zmienna string ejst niemodyfikalna co widać na poniższym przykładzie
            string upperString = s5.ToUpper();
            Console.WriteLine(s5);
            Console.WriteLine(upperString);
            Console.WriteLine(s5);

            StringBuilder sbPopularSeries = new StringBuilder("Popularne seriale"); //metoda StringBuilder jest modyfikowalna co widać na poniższym przykładzie
            sbPopularSeries.Append("\n");
            sbPopularSeries.AppendLine("Przyjaciele");
            sbPopularSeries.AppendLine("Lucyfer");
            sbPopularSeries.AppendLine("Skazany na śmierć 5");
            Console.WriteLine(sbPopularSeries.ToString());
            Console.WriteLine(sbPopularSeries.Length);
            Console.WriteLine(sbPopularSeries.Replace("5", " Ostatnia ucieczka"));
            Console.WriteLine(sbPopularSeries.Length);

            Console.ReadKey();
        }
    }
}
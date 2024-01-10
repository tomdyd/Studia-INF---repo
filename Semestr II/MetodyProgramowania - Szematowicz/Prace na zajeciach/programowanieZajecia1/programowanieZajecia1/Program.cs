using System;

namespace programowanieZajecia1
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = Stanowisko.Majster;
            var prem = GetPremia(st);
            Console.WriteLine(prem);
            int a = 25;
            Stanowisko st2 = Stanowisko.Dyrektor;

            int[] arrayOfInt = new int[]
            {
                52, 7, 3, 58, 98
            };

            //stanowisko[] stanowiskos = new stanowisko[]
            //{
            //    stanowisko.dyrektor,
            //    stanowisko.kierownik,
            //    stanowisko.kadrowy,
            //    stanowisko.księgowy,
            //    stanowisko.majster,
            //};
            Print();

            Console.WriteLine("--------------------------------------------------------");
            Print(arrayOfInt);
            Console.WriteLine("--------------------------------------------------------");
            //Print(stanowiskos);
            Console.Write("Podaj swoje stanowisko: ");
            var userStanowiskoStr = GetStanowisko();

            Console.WriteLine("Twoja premia to " + GetPremia(userStanowiskoStr));

        }

        static Stanowisko GetStanowisko()
        {
            while (true)
            {
                var str = Console.ReadLine();
                object obj;
                bool isStanoiwsko = Enum.TryParse(typeof(Stanowisko), str, out obj);
                if (isStanoiwsko)
                    return (Stanowisko)obj;
                Console.WriteLine("To nie jest stanowisko, wpisz jeszcze raz");
            }
        }
        static void Print(Stanowisko[] array)
        {
            foreach (var item in array)
            {
                Console.WriteLine("{0} - {1}", (int)item, item);
            }
        }
        static void Print(int[] array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            Array.Sort(array);
            Console.WriteLine("--------------------------------------------------------");
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        static void Print()
        {
            var array = Enum.GetValues(typeof(Stanowisko));
            foreach (var item in array)
            {
                Console.WriteLine("{0} - {1}", (int)item, item);
            }
        }
        static decimal GetPremia(Stanowisko stanowisko)
        {
            switch (stanowisko)
            {
                case Stanowisko.Dyrektor: return 80;
                case Stanowisko.Kierownik: return 70;
                case Stanowisko.Kadrowy: return 45;
                case Stanowisko.Księgowy: return 30;
                case Stanowisko.Majster: return 25;
                default: return 5;
            }
        }
    }
}


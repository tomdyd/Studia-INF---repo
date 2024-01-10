using ProgramowanieObiektowe;
using System;

namespace zajeciaProgramowanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 a = 25;
            Pracownik dyrektor;
            dyrektor._firstName = "Jacek";
            dyrektor._lastName = "Placek";
            dyrektor._stanowisko = Stanowisko.Dyrektor;
            dyrektor._age = 25;
            dyrektor._email = "dyrektor@gmail.com";

            var info = dyrektor.GetInfo();
            Console.WriteLine(info);

            dyrektor.IncreaseAge();
            info = dyrektor.GetInfo();
            Console.WriteLine(info);
            Console.WriteLine("------------------------------");

            int b = new int();
            Pracownik pracownik = new Pracownik();
            Pracownik pracownik2 = new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek@gmail.com", Stanowisko.Kadrowy);
            

            var intInfo = b.ToString();
            var pracownikinfo = pracownik.GetInfo();

        }
    }
}
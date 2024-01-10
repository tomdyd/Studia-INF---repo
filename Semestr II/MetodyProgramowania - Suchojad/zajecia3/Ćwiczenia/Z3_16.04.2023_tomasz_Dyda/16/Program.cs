using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Podaj liczbę dziesiętną: ");
        int liczbaDziesietna = int.Parse(Console.ReadLine());

        string liczbaSzesnastkowa = ZamienNaSzesnastkowa(liczbaDziesietna);

        Console.WriteLine("Liczba szesnastkowa: " + liczbaSzesnastkowa);
    }

    static string ZamienNaSzesnastkowa(int liczbaDziesietna)
    {
        string cyfrySzesnastkowe = "0123456789ABCDEF";
        string wynik = "";

        while (liczbaDziesietna > 0)
        {
            int reszta = liczbaDziesietna % 16;
            wynik = cyfrySzesnastkowe[reszta] + wynik;
            liczbaDziesietna /= 16;
        }

        return wynik;
    }

}


using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Wybierz operację:");
        Console.WriteLine("1. Konwersja liczby arabskiej na rzymską");
        Console.WriteLine("2. Konwersja liczby rzymskiej na arabską");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                ArabictoRoman();
                break;
            case 2:
                RomantoArabic();
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór.");
                break;
        }
    }

    static void ArabictoRoman()
    {
        Console.WriteLine("Podaj liczbę arabską (od 1 do 3999):");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number < 1 || number > 3999)
        {
            Console.WriteLine("Nieprawidłowa liczba arabska.");
            return;
        }

        string romanNumber = ConvertToRoman(number);

        Console.WriteLine("Liczba rzymska: " + romanNumber);
    }

    static void RomantoArabic()
    {
        Console.WriteLine("Podaj liczbę rzymską (od I do MMMCMXCIX):");
        string romanNumber = Console.ReadLine();

        int arabicNumber = ConvertToArabic(romanNumber);

        if (arabicNumber == -1)
        {
            Console.WriteLine("Nieprawidłowa liczba rzymska.");
        }
        else
        {
            Console.WriteLine("Liczba arabska: " + arabicNumber);
        }
    }

    static string ConvertToRoman(int number)
    {
        Dictionary<int, string> romanDict = new Dictionary<int, string>()
        {
            {1000, "M"},
            {900, "CM"},
            {500, "D"},
            {400, "CD"},
            {100, "C"},
            {90, "XC"},
            {50, "L"},
            {40, "XL"},
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"}
        };

        string romanNumber = "";

        foreach (var pair in romanDict)
        {
            while (number >= pair.Key)
            {
                romanNumber += pair.Value;
                number -= pair.Key;
            }
        }

        return romanNumber;
    }

    static int ConvertToArabic(string romanNumber)
    {
        Dictionary<char, int> romanDict = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        int arabicNumber = 0;
        int prevValue = 0;

        for (int i = romanNumber.Length - 1; i >= 0; i--)
        {
            if (!romanDict.ContainsKey(romanNumber[i]))
            {
                return -1; // Nieprawidłowa liczba rzymska
            }

            int currentValue = romanDict[romanNumber[i]];

            if (currentValue >= prevValue)
            {
                arabicNumber += currentValue;
            }
            else
            {
                arabicNumber -= currentValue;
            }

            prevValue = currentValue;
        }

        return arabicNumber;
    }
}

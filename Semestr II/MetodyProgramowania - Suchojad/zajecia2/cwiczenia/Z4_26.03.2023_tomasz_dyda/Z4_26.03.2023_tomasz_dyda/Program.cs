using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Wybierz operację:");
        Console.WriteLine("1. Konwersja liczby dziesiętnej na system silniowy");
        Console.WriteLine("2. Konwersja liczby w systemie silniowym na dziesiętną");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                DecimalToFactorial();
                break;
            case 2:
                FactorialToDecimal();
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór.");
                break;
        }
    }

    static void DecimalToFactorial()
    {
        Console.WriteLine("Podaj liczbę dziesiętną:");
        int number = Convert.ToInt32(Console.ReadLine());

        string factorialNumber = ConvertToFactorial(number);

        Console.WriteLine("Liczba w systemie silniowym: " + factorialNumber);
    }

    static void FactorialToDecimal()
    {
        Console.WriteLine("Podaj liczbę w systemie silniowym:");
        string factorialNumber = Console.ReadLine();

        int decimalNumber = ConvertToDecimal(factorialNumber);

        Console.WriteLine("Liczba dziesiętna: " + decimalNumber);
    }

    static string ConvertToFactorial(int number)
    {
        List<int> factorialDigits = new List<int>();

        int divisor = 2;

        while (number > 0)
        {
            int digit = number % divisor;
            factorialDigits.Add(digit);
            number = (number - digit) / divisor;
            divisor++;
        }

        factorialDigits.Reverse();

        string factorialNumber = "";

        foreach (int digit in factorialDigits)
        {
            factorialNumber += digit.ToString();
        }

        return factorialNumber;
    }
    static int ConvertToDecimal(string factorialNumber)
    {
        int decimalNumber = 0;

        for (int i = 0; i < factorialNumber.Length-1; i++)
        {
            int digit = int.Parse(factorialNumber[i].ToString());

            decimalNumber += CalculateFactorial(factorialNumber.Length-i) * digit;
        }

        return decimalNumber;
    }
    static int CalculateFactorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException("Liczba nie może być ujemna.");
        }

        if (number == 0 || number == 1)
        {
            return 1;
        }
        else
        {
            return number * CalculateFactorial(number - 1);
        }
    }
}

using System;

class PrimeNumberChecker
{
    static void Main()
    {
        Console.Write("Enter an integer greater than 1: ");
        bool isNumber = int.TryParse(Console.ReadLine(), out int number);
        while(!isNumber || number < 2)
        {
            Console.Clear();
            Console.Write("Enter an integer greater than 1: ");
            isNumber = int.TryParse(Console.ReadLine(), out number);
        }

        bool isPrime = IsPrimeNumber(number);
        bool isSuperPrime = false;

        // Warunek sprawdza czy liczba może być super pierwsza - liczba musi mieć co najmniej dwie cyfry czyli > 9
        if (number > 9)
        {
            isSuperPrime = IsSuperPrimeNumber(number);
        }


        if (isSuperPrime)
        {
            Console.WriteLine(number + " is a super prime number.");
        }
        else if (isPrime)
        { 
            Console.WriteLine(number + " is a prime number.");
        }        
        else
        {
            Console.WriteLine(number + " is not a prime number.");
        }
    }

    static bool IsPrimeNumber(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    static bool IsSuperPrimeNumber(int number)
    {
        int temp = number;
        int sum = 0;
        while (temp > 0)
        {
            // Sumujemy liczbę cyfr wprowadzonej liczby (mod 10 daje ostatnią cyfrę liczby)
            sum += temp % 10;
            temp /= 10;
        }
        // Sprawdzamy czy suma cyfr liczby pierwszej jest również liczbą pierwszą
        bool isSuperPrime = IsPrimeNumber(sum);
        return isSuperPrime;
    }
}

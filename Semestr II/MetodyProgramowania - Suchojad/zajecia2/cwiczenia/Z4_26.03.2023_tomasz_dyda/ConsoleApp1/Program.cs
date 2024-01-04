long CalculateFactorial(long number)
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

long number = 4;

number = CalculateFactorial(number);
Console.WriteLine(number);
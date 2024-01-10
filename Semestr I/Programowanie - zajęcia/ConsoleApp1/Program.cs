using System;

Console.Write("Podaj liczbę kolumn z zakresu od 1 do 10: ");
int i = int.Parse(Console.ReadLine());
Console.Write("Podaj liczbę wierszy z zakresu od 1 do 10: ");
int j = int.Parse(Console.ReadLine());

int[,] a = new int[i, j];
int[,] b = new int[i, j];
int[,] c = new int[i, j];
Random generator = new Random();

Console.WriteLine("\nTABLICA A: ");
for (int k = 0; k < i; k++)
{
    for (int l = 0; l < j; l++)
    {
        a[k, l] = generator.Next(0, 10);
        Console.Write($"{a[k, l]} ");
    }
    Console.WriteLine();
}

Console.WriteLine("\nTABLICA B: ");
for (int k = 0; k < i; k++)
{
    for (int l = 0; l < j; l++)
    {
        b[k, l] = generator.Next(0, 10);
        Console.Write($"{b[k, l]} ");
    }
    Console.WriteLine();
}

Console.WriteLine("\nTABLICA C: ");
for (int k = 0; k < i; k++)
{
    for (int l = 0; l < j; l++)
    {
        c[k, l] = a[k, l] + b[k, l];
        Console.Write($"{c[k, l]} ");
    }
    Console.WriteLine();
}
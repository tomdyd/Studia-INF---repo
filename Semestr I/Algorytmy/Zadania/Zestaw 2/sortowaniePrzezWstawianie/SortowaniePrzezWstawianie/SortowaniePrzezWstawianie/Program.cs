﻿using System.Security.Cryptography.X509Certificates;

Console.Write("Podaj liczbę elementów zbioru: ");
bool isNumber = int.TryParse(Console.ReadLine(), out int userInput);

while(!isNumber)
{
    Console.WriteLine("PODAJ LICZBĘ!");
    isNumber = int.TryParse(Console.ReadLine(), out userInput);
}

while(userInput <= 0)
{
    Console.WriteLine("Podana liczba musi być dodatnia!");
    isNumber = int.TryParse(Console.ReadLine(), out userInput);
}

int[] T = new int[userInput];

for(int i = 0; i <= T.Length - 1; i++)
{
    Console.Write($"Podaj {i+1} element zbioru: ");
    T[i] = int.Parse(Console.ReadLine());
}

int ile = T.Length;

for (int i = ile - 1; i >= 0 ; i--)
{
    int x = T[i];
    int j = i + 1;

    while (j < ile && x > T[j])
    {
        T[j - 1] = T[j];
        j = j + 1;
        T[j - 1] = x;
    }
}

for (int i = 0; i <= T.Length - 1; i++)
{
    Console.WriteLine(T[i]);
}


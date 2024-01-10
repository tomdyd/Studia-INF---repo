using System;

class Program
{
    static void Main()
    {
        int totalStudents = 75;
        int maxEmptySeats = 5;

        int minCost = int.MaxValue;
        int optimalA = 0, optimalB = 0, optimalC = 0;

        for (int numA = 0; numA <= totalStudents; numA += 9)
        {
            for (int numB = 0; numB <= totalStudents - numA; numB += 18)
            {
                int numC = totalStudents - numA - numB;
                int emptySeats = 32 - numC;

                if (emptySeats <= maxEmptySeats)
                {
                    int cost = (numA / 9) * 300 + (numB / 18) * 550 + (numC / 32) * 900;

                    if (cost < minCost)
                    {
                        minCost = cost;
                        optimalA = numA / 9;
                        optimalB = numB / 18;
                        optimalC = numC / 32;
                    }
                }
            }
        }

        Console.WriteLine("Optymalne rozwiązanie:");
        Console.WriteLine("Pojazd A (9 miejsc): " + optimalA);
        Console.WriteLine("Pojazd B (18 miejsc): " + optimalB);
        Console.WriteLine("Pojazd C (32 miejsca): " + optimalC);
        Console.WriteLine("Koszt przewozu: " + minCost + " zł");
    }
}

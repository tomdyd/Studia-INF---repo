using System;

class Program
{
    static void Main(string[] args)
    {
        double maxTargetVolume = 154.0;
        double costFloor = 100.0;  // koszt za 1 m2 dna
        double costSide = 75.0;   // koszt za 1 m2 ściany bocznej
        double bestCost = double.MaxValue;
        double bestWidth = 0;
        double bestHeight = 0;

        // Przeszukiwanie możliwych wymiarów zbiornika
        for (double width = 1; width <= 9; width += 0.001)
        {
            for (double height = 1; height <= 9; height += 0.001)
            {
                double volume = width * width * height;
                double cost = costFloor * (width * width) + 4 * (width * height * costSide);

                // Sprawdzenie, czy objętość jest równa docelowej pojemności i czy koszt jest niższy od dotychczasowego najlepszego kosztu
                if (Math.Abs(volume - maxTargetVolume) < 0.001 && cost < bestCost)
                {
                    bestCost = Math.Round(cost, 2);
                    bestWidth = Math.Round(width, 3);
                    bestHeight = Math.Round(height, 3);
                }
            }            
        }

        // Wyświetlenie wyników
        Console.WriteLine("Najlepszy koszt: " + bestCost + " zł");
        Console.WriteLine("Szerokość: " + bestWidth + " m");
        Console.WriteLine("Długość: " + bestWidth + " m");
        Console.WriteLine("Wysokość: " + bestHeight + " m");
    }
}

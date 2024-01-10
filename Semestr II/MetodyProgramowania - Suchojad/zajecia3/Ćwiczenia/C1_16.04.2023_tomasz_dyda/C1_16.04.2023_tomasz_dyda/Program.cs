double[] money = new double[] { 500, 200, 100, 50, 20, 10, 5, 2, 1, 0.5, 0.2, 0.1, 0.05, 0.02, 0.01 };

MainMenu();

#region funkcje
void MainMenu()
{  
    while (true)
    {
        Console.Clear();
        Console.WriteLine("MENU");
        Console.WriteLine("---------");
        Console.WriteLine("1. Tablica nominałów");
        Console.WriteLine("2. Wydawanie metodą inkrementacyjną");
        Console.WriteLine("3. Wyjście z programu");

        Console.Write("\nWybierz opcje: ");
        int number = int.Parse(Console.ReadLine());
        Console.Clear();

        switch (number)
        {
            case 1:
                TablicaNominalow();
                break;
            case 2:
                MetodaInkrementacyjna();
                break;

            case 3:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}
void TablicaNominalow()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Dostępne nominały: ");
        foreach (double nominal in money)
        {
            Console.WriteLine(nominal);
        }
        Console.WriteLine("\n1. Wróć");

        Console.Write("Wybierz opcje: ");
        int number = int.Parse(Console.ReadLine());

        switch (number)
        {
            case 1: MainMenu(); break;
            default: break;
        }
    }
}
void MetodaInkrementacyjna()
{    
    Console.Write("Podaj resztę: ");
    decimal r = decimal.Parse(Console.ReadLine());
    decimal temp = r;
    while (true)
    {
        r = temp;
        Console.Clear();
        foreach (decimal nominal in money)
        {
            if (r == nominal)
            {
                Console.WriteLine($"Wydano 1 monetę po {nominal}zł.");
                break;
            }

            else if (r > nominal || r == nominal)
            {
                decimal tmp = r / nominal;
                int tmp1 = (int)tmp;
                Console.WriteLine($"Wydano {tmp1} monety po {nominal}zł.");
                r = r - tmp1 * nominal;
            }
        }

        Console.WriteLine("\n1. Wróć");
        Console.Write("Wybierz opcje: ");
        int number = int.Parse(Console.ReadLine());
        switch (number)
        {
            case 1: MainMenu(); break;
            default: Console.WriteLine("Brak opcji!"); break;
        }
    }
}
#endregion
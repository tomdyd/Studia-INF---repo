List<int> array = new List<int>();

Console.WriteLine("Podaj z ilu liczb składa się ciąg: ");
int n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    Console.WriteLine($"Podaj {i + 1}. wyraz ciągu");
    array.Add(int.Parse(Console.ReadLine()));
}

for (int i = 0; i < 2; i++)
{
    foreach (int item in array)
    {
        Console.Write($"{item}, ");
    }
    Console.WriteLine();

    array.RemoveRange(0, 1);
}
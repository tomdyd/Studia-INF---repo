using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

Console.Write("Set number between 1 to 1000 of elements in array: ");
bool isNumber = int.TryParse(Console.ReadLine(), out int n);

if (n == 1)
{
    Console.WriteLine("There is no tape to cut!");
    System.Environment.Exit(0);
}

while (!isNumber || n <= 0 || n > 1000)
{
    Console.Clear();
    Console.WriteLine("You put invalid value. Try again.");
    Console.Write("Set number between 1 to 1000 of elements in array: ");
    isNumber = int.TryParse(Console.ReadLine(), out n);
}

int[] array = new int[n];

int sum = 0;
do
{
    if (array.Max() > 100)
    {
        Console.WriteLine("Max value is 100!");
    }
    else if (array.Min() < -100)
    {
        Console.WriteLine("Min value is -100!");
    }

    string[] data = Validate();

    for (int i = 0; i < n; i++)
    {
        array[i] = int.Parse(data[i]);
        sum += array[i];
    }
} while (array.Max() > 100 || array.Min() < -100);

int temp = 0;
int difference = Math.Abs(array[0] - sum / 2);
int elementNumber = 0;

for (int i = 0; i < array.Length; i++)
{
    temp += array[i];
    int temp1 = Math.Abs(temp - sum / 2);

    if (temp1 < difference) //jesli po dodaniu kolejnej liczby z tablicy wartosc bezwgledna roznicy jest mniejsza to zastepujemy
    {                      //podmieniamy na ta wartosc w zmiennej difference
        difference = temp1;
    }

    else if (temp1 > difference) //jesli po dodaniu kolejnej liczby z tablicy wartosc bezwgledna roznicy jest wieksza to znaczy ze 
    {                           //poprzednia wartosc roznicy jest najmniejsza mozliwa, wiec przerywamy petle
        temp -= array[i]; //pierwsza czesc "tasmy", nalezy odjac ostatnio dodany element poniewaz dodajac go,
                          // przekroczylismy najmniejsza mozliwa roznice
        temp1 = array.Sum() - temp;
        difference = Math.Abs(temp1 - temp);
        elementNumber = i; //przypisanie elementu po ktorym nalezy "odciac tasme",
        break;             //poniewaz iteracja jest od 0 to nie musimy "cofac" elementu (i - 1)
    }
}

Console.WriteLine();
Console.WriteLine($"The tape have to be cut after element: {elementNumber}");
Console.WriteLine($"The difference between tapes is: {difference}");

string[] Validate()
{
    Console.WriteLine("List the elements of an array, separating them with a space (min value -100, max value 100): ");
    string input = Console.ReadLine();
    string[] data = input.Split(" ");
    string regex = "^(\\d+)(\\s\\d+)*$";

    data = Reg(input, regex, data);

    while (data.Length != n)
    {
        if (data.Length > n)
        {
            data = DataError("many", input, data);

            data = Reg(input, regex, data);
        }
        else if (data.Length < n)
        {
            data = DataError("low", input, data);

            data = Reg(input, regex, data);
        }
    }
    return data;
}

string[] Reg(string input, string regex, string[] data)
{
    while (!Regex.IsMatch(input, regex))
    {
        Console.WriteLine("List the elements of an array, separating them with a space (min value -100, max value 100): ");
        input = Console.ReadLine();
        data = input.Split(" ");
    }
    return data;
}

string[] DataError(string message, string input, string[] data)
{
    Console.Clear();
    Console.WriteLine($"Too {message} data! Try again.");
    Console.WriteLine("List the elements of an array, separating them with a space: ");
    input = Console.ReadLine();
    data = input.Split(" ");
    return data;
}

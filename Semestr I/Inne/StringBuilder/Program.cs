using System.ComponentModel.DataAnnotations;

double number;
bool isNumber;
int i = 0;
double[] tries = new double[1000];

do
{
    isNumber = double.TryParse(Console.ReadLine(), out number);

    if (!isNumber)
    {
        Console.Write("Incorrect value! Try again: ");
        isNumber = double.TryParse(Console.ReadLine(), out number);
    }

    tries[i] = number;
    i++;

} while (number != 0);

double maxValue = tries.Max();

Console.WriteLine($"Podałeś wartość 0 za {i} razem");
Console.WriteLine($"Największa wartość {maxValue}");
Console.ReadKey();
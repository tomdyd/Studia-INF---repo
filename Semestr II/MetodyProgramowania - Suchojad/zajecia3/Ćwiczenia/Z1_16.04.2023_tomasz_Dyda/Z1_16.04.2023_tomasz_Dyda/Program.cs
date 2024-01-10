Console.WriteLine("Podaj z ilu liczb składa się ciąg: ");
int n = int.Parse(Console.ReadLine());
Console.Clear();
List<int> array = new List<int>();

for (int i = 0; i < n; i++)
{
    Console.WriteLine($"Podaj {i + 1}. wyraz ciągu");
    array.Add(int.Parse(Console.ReadLine()));
    Console.Clear();
}

int cost = 0;
int numberOfMerging = 0;
int numberOfOperations = 0;

while (array.Count != 1)
{
    InsertionSort(array, numberOfOperations);
    int temp = MergeFirstTwo(array);
    cost += temp;
    numberOfMerging++;
}

foreach (int item in array)
{
    Console.WriteLine($"Suma liczb = {item}");
    Console.WriteLine($"Koszt sklejania = {cost}");
    Console.WriteLine($"Liczba scaleń = {numberOfMerging}");
    Console.WriteLine($"Liczba wykonanych operacji = {numberOfOperations}");
}

void InsertionSort(List<int> array, int operationsCounter)
{
    int n = array.Count;
    for (int i = 1; i < n; ++i)
    {
        int key = array[i];
        int j = i - 1;
        numberOfOperations += 2;

        // Przesuń elementy większe od klucza w prawo
        while (j >= 0 && array[j] > key)
        {
            array[j + 1] = array[j];
            j = j - 1;
            numberOfOperations++;
        }
        array[j + 1] = key;
        numberOfOperations++;
    }
}
int MergeFirstTwo(List<int> list)
{
    int sum = list[0] + list[1]; // zsumuj pierwsze dwie liczby
    list.RemoveRange(0, 2); // usuń dwie pierwsze liczby z listy
    list.Add(sum); // wstaw zsumowaną wartość na początek listy
    return sum;
}
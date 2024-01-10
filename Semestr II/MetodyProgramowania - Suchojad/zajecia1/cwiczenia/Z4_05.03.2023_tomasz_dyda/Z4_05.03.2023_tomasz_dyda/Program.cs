using System.Data;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.ExceptionServices;

int[] array = new int[50000];

Random randGenerator = new();
Stopwatch stopwatch = new();

Console.WriteLine("Wybierz opcje:");
Console.WriteLine("1. Optymalny zestaw danych");
Console.WriteLine("2. Losowy zestaw danych");
Console.WriteLine("3. Nieoptymalny zestaw danych");

int number = int.Parse(Console.ReadLine());

switch (number)
{
    case 1:
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }
        break;

    case 2:
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = randGenerator.Next(1, 50001);
        }
        break;
    
    case 3:
        int j = 0;
        for (int i = array.Length-1; i >= 0; i--)
        {
            array[j] = i;
            j++;
        }
        break;
}; //Wybranie zestawu danych

Console.WriteLine("SORTOWANIE BĄBELKOWE");
Console.WriteLine("--------------------");
int[] bubbleSortArray = new int[array.Length];
for (int i = 0; i < bubbleSortArray.Length; i++)
{
    // Kopiowanie tablicy
    bubbleSortArray[i] = array[i];
} 

BubbleSort(bubbleSortArray);

Console.WriteLine("\nSORTOWANIE PRZEZ WSTAWIANIE");
Console.WriteLine("---------------------------");

int[] insertionSortArray = new int[array.Length];
for (int i = 0; i < insertionSortArray.Length; i++)
{
    // Kopiowanie tablicy
    insertionSortArray[i] = array[i];
} 

InsertionSort(array);

int[] mergeSortArray = new int[array.Length];
for(int i = 0; i < mergeSortArray.Length; i++)
{
    // Kopiowanie tablicy
    mergeSortArray[i] = array[i];
}

Console.WriteLine("\nSORTOWANIE PRZEZ SCALANIE");
Console.WriteLine("---------------------------");
stopwatch.Reset();
stopwatch.Start();

var resultMergeSort = MergeSort(mergeSortArray);
stopwatch.Stop();
TimeSpan timeElapsed = stopwatch.Elapsed;
Console.WriteLine("Ilość porównań: " + resultMergeSort.comparisons);
Console.WriteLine("Ilość zamian: " + resultMergeSort.swaps);
Console.WriteLine("Czas działania: " + timeElapsed);

int[] selectionSortArray = new int[array.Length];
for (int i = 0; i < selectionSortArray.Length; i++)
{
    // Kopiowanie tablicy
    selectionSortArray[i] = array[i];
}

Console.WriteLine("\nSORTOWANIE PRZEZ WYBÓR");
Console.WriteLine("---------------------------");
stopwatch.Reset();
stopwatch.Start();
var resultSelectionSort = SelectionSort(selectionSortArray);

stopwatch.Stop();
TimeSpan timeElapsedSelectionSort = stopwatch.Elapsed;

Console.WriteLine("Ilość porównań: " + resultSelectionSort.comparisons);
Console.WriteLine("Ilość zamian: " + resultSelectionSort.swaps);
Console.WriteLine("Czas działania: " + timeElapsedSelectionSort);



#region metody
void BubbleSort(int[] array)
{
    int zamiana = 0;
    int porownania = 0;

    stopwatch.Reset();
    stopwatch.Start();
    for (int i = 0; i < array.Length; i++)
    {
        for (int j = 0; j < array.Length; j++)
        {
            if (i == j)
                break;

            porownania++;
            if (array[j] > array[i])
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                zamiana++;
            }
        }
    }
    stopwatch.Stop();

    Console.WriteLine($"Ilość porównań: {porownania}");
    Console.WriteLine($"Ilość zamian: {zamiana}");

    TimeSpan ts = stopwatch.Elapsed;
    Console.WriteLine("Czas działania: {0:00}:{1:00}:{2:00}.{3}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
} //metoda sortowanie bąbelkowego
void InsertionSort(int[] array)
{
    double zamiana = 0;
    double porownania = 0;

    stopwatch.Reset();
    stopwatch.Start();
    for (int i = 1; i < array.Length; i++)
    {
        int j = i;
        int temp = array[j];
        while (j > 0 && array[j-1] > temp)
        {
            array[j] = array[j-1];
            j--;
            zamiana++;
            porownania++;
        }
        array[j] = temp;

        if (j > 0 && array[j-1] < temp)
        porownania++;
    }
    stopwatch.Stop();

    Console.WriteLine($"Ilość porównań: {porownania}");
    Console.WriteLine($"Ilość zamian: {zamiana}");

    TimeSpan ts = stopwatch.Elapsed;
    Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
} //metoda sortowania przez wstawianie

SortResult MergeSort(int[] arr)
{
    SortResult result = new SortResult();
    result.comparisons = 0;
    result.swaps = 0;

    int[] tempArray = new int[arr.Length];
    Array.Copy(arr, tempArray, arr.Length);

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    int[] sortedArray = MergeSortRecursive(tempArray, 0, arr.Length - 1, result);

    stopwatch.Stop();
    result.sortedArray = sortedArray;
    result.timeElapsed = stopwatch.Elapsed;

    return result;
}
int[] MergeSortRecursive(int[] arr, int left, int right, SortResult result)
{
    if (left < right)
    {
        int mid = (left + right) / 2;
        int[] leftArray = MergeSortRecursive(arr, left, mid, result);
        int[] rightArray = MergeSortRecursive(arr, mid + 1, right, result);
        return Merge(leftArray, rightArray, result);
    }

    return new int[] { arr[left] };
}
int[] Merge(int[] leftArray, int[] rightArray, SortResult result)
{
    int[] mergedArray = new int[leftArray.Length + rightArray.Length];
    int i = 0, j = 0, k = 0;

    while (i < leftArray.Length && j < rightArray.Length)
    {
        result.comparisons++;

        if (leftArray[i] <= rightArray[j])
        {
            mergedArray[k++] = leftArray[i++];
        }
        else
        {
            mergedArray[k++] = rightArray[j++];
        }

        result.swaps++;
    }

    while (i < leftArray.Length)
    {
        mergedArray[k++] = leftArray[i++];
        result.swaps++;
    }

    while (j < rightArray.Length)
    {
        mergedArray[k++] = rightArray[j++];
        result.swaps++;
    }

    return mergedArray;
}
SortResult SelectionSort(int[] arr)
{
    SortResult result = new SortResult();
    result.comparisons = 0;
    result.swaps = 0;

    int n = arr.Length;
    int[] sortedArray = new int[n];
    Array.Copy(arr, sortedArray, n);

    for (int i = 0; i < n - 1; i++)
    {
        int minIndex = i;

        for (int j = i + 1; j < n; j++)
        {
            result.comparisons++;

            if (sortedArray[j] < sortedArray[minIndex])
            {
                minIndex = j;
            }
        }

        if (minIndex != i)
        {
            Swap(sortedArray, i, minIndex);
            result.swaps++;
        }
    }

    result.sortedArray = sortedArray;
    return result;
}

static void Swap(int[] arr, int i, int j)
{
    int temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
}
class SortResult
{
    public int[] sortedArray;
    public int comparisons;
    public int swaps;
    public TimeSpan timeElapsed;
}
#endregion
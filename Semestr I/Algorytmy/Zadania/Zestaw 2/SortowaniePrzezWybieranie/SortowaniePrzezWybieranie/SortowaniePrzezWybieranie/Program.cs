using System.Globalization;

int[] T = new int[5];
T[0] = 10;
T[1] = 1;
T[2] = 5;
T[3] = 4;
T[4] = 2;

int howMany = T.Length;
int temp;

for (int i = 0; i < howMany; i++)
{
    temp = T[i];

    for (int j = i; j < howMany; j++)
    {
        if (temp > T[j])
        {
            temp = T[j];
            T[j] = T[i];
            T[i] = temp;

        }
    }
}

for (int i = 0; i < howMany; i++)
{
    Console.WriteLine(T[i]);
}

int fenceHeight = 4;
string userInput = "HELLO WORLD"; 

char[,] fence = new char[fenceHeight, userInput.Length];

int row = 0;
int direction = 1;
int[] array = new int[fenceHeight];
string[] strArray = new string[fenceHeight];

for (int i = 0; i < userInput.Length; i++)
{
    array[row]++;

    if (row == fenceHeight - 1)
        direction = -1;
    else if (row == 0)
        direction = 1;
    row += direction;
}

foreach (int a in array)
{
    Console.WriteLine(a);
}

int j = 0;

for (row = 0; row < fenceHeight; row++)
{
    for(int i = 0; i < array[row]; i++)
    {
        strArray[row] += userInput[j]; //podział słowa na litery według ilości wystąpień w rzędzie tabeli
        j++;
    }
}

int[] indexArray = new int[fenceHeight];
row = 0;

for (int i = 0; i < userInput.Length; i++)
{
    string temp = strArray[row];

    fence[row, i] = temp[indexArray[row]];
    indexArray[row]++;

    if (row == fenceHeight - 1)
        direction = -1;
    else if (row == 0)
        direction = 1;
    row += direction;
}

string decryptedMessage = "";

for (int i = 0; i < userInput.Length; i++)
{
    for (int k = 0; k < fenceHeight; k++)
    {
        if (fence[k, i] != '.')
        {
            decryptedMessage += fence[k, i];
        }
    }
}

Console.WriteLine(decryptedMessage);

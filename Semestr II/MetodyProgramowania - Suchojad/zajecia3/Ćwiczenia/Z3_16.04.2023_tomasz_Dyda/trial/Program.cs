using System.ComponentModel;

Console.WriteLine("The card number consists of numbers from 0 to 9");
Console.Write("Enter the card number: ");

string input = Console.ReadLine();
input = input.Trim().Replace(" ", "");
decimal cardNumber = decimal.Parse(input);

string result = ChangeToHexadecimal(cardNumber);
Console.WriteLine(result);

string conversionArray = ("0123456789012345");
string PIN = "";

for (int i = 0; i < 4; i++)
{
    int temp = result[i];
    PIN = conversionArray[temp] + PIN;
}

Console.WriteLine(PIN);

string ChangeToHexadecimal(decimal cardNumber)
{

    string hexadecimalDigits = "0123456789ABCDEF";
    string result = "";

    while (cardNumber > 0)
    { 
        decimal temp = cardNumber % 16;
        int temp1 = (int)temp;

        if (temp1 != 0)
        {
            result = hexadecimalDigits[temp1] + result;
        }

        cardNumber = cardNumber / 16;
    }

    return result;
}



﻿using System.Text.RegularExpressions;

Console.WriteLine("The card number consists of numbers from 0 to 9");
Console.Write("Enter the card number: ");
string input = Console.ReadLine();

var regex = new Regex(@"^[0-9 	]+$");
while(!regex.IsMatch(input))
{
    Console.Clear();
    Console.WriteLine("The card number must consists only of number from 0 to 9!");
    Console.Write("Enter the card number: ");
    input = Console.ReadLine();
}

input = input.Trim().Replace(" ", "");
input = input.Trim().Replace("\u0009", "");

decimal cardNumber = decimal.Parse(input);
string result = ChangeToHexadecimal(cardNumber);

Console.WriteLine("\nEncrypted card number is " + result);

int[] PINarray = new int[4];

for (int i = 0; i < PINarray.Length; i++)
{
    switch (result[i])
    {
        case '1':
            PINarray[i] = 0;
            break;
        case '2':
            PINarray[i] = 1;
            break;
        case '3':
            PINarray[i] = 2;
            break;
        case '4':
            PINarray[i] = 3;
            break;
        case '5':
            PINarray[i] = 4;
            break;
        case '6':
            PINarray[i] = 5;
            break;
        case '7':
            PINarray[i] = 6;
            break;
        case '8':
            PINarray[i] = 7;
            break;
        case '9':
            PINarray[i] = 8;
            break;
        case 'A':
            PINarray[i] = 9;
            break;
        case 'B':
            PINarray[i] = 0;
            break;
        case 'C':
            PINarray[i] = 1;
            break;
        case 'D':
            PINarray[i] = 2;
            break;
        case 'E':
            PINarray[i] = 3;
            break;
        case 'F':
            PINarray[i] = 4;
            break;
    }
}

string PIN = string.Join("", PINarray);
Console.WriteLine("Your PIN code is: " + PIN);
static string ChangeToHexadecimal(decimal cardNumber)
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
        cardNumber /= 16;
    }

    return result;
}

using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            SetCounter(dictionary);

            string input = "ala ma kota";

            foreach(char c in input)
            {
                dictionary[c]++;
            }

            foreach(var c in dictionary)
            {
                if(c.Key != ' ')
                Console.WriteLine($"Znak {c.Key}: {c.Value} wystapien");
                else
                {
                    Console.WriteLine($"Spacja: {c.Value} wystapien");
                }
            }
        }

        static void SetCounter(Dictionary<char, int> dictionary)
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                dictionary[c] = 0;
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                dictionary[c] = 0;
            }

            for (char c = '0'; c <= '9'; c++)
            {
                dictionary[c] = 0;
            }

            dictionary['.'] = 0;
            dictionary[','] = 0;
            dictionary[' '] = 0;
        }
    }    
}

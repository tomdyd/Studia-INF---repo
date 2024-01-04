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

            // Wypisanie wszystkich kluczy ze słownika wraz z ilością wystąpień
            foreach (var kvp in dictionary)
            {
                if(kvp.Key != ' ')
                Console.WriteLine($"{kvp.Key}: {kvp.Value} wystąpień");
            }

            // Wypisanie ilości wystąpień spacji
            Console.WriteLine($"Spacja: {dictionary[' ']} wystąpień");
        }

        public static void SetCounter(Dictionary<char, int> dictionary)
        {
            dictionary['.'] = 0;
            dictionary[','] = 0;
            dictionary[' '] = 0;
            for (char c = '0'; c <= '9'; c++)
            {
                dictionary[c] = 0;
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                dictionary[c] = 0;
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                dictionary[c] = 0;
            }
        }
    }
}

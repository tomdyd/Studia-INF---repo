using System;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAppInfo();

            string username = GetUserName();

            GreetUser(username);

            Random random = new Random();

            int CorrectNumber = random.Next(1, 11);

            bool CorrectAnswer = false;

            Console.WriteLine("Zgadnij wylosowaną liczbę z przedziału od 1 do 10.");

            for (int Tries = 1;  !CorrectAnswer; Tries++) // !CorrectAnswer to to samo co CorrectAnswer == false
            {
                string input = Console.ReadLine();

                int guess;

                bool isNumber = int.TryParse(input, out guess);

                if (!isNumber)
                {
                    PrintColorMessage(ConsoleColor.Yellow, "Proszę wporwadzić liczbę!");
                    continue;
                }

                if (guess < 1 || guess > 10)
                {
                    PrintColorMessage(ConsoleColor.Yellow, "Proszę wprowadzić liczbę z przedziału od 1 do 10");
                    continue;
                }

                if (guess < CorrectNumber)
                {
                    PrintColorMessage(ConsoleColor.Red, "Błąd. Wylosowana liczba jest większa!");
                }

                else if (guess > CorrectNumber)
                {
                    PrintColorMessage(ConsoleColor.Red, "Błąd. Wylosowana liczba jest mniejsza!");
                }


                else
                {
                    CorrectAnswer = true;
                    PrintColorMessage(ConsoleColor.Green, $"Brawo! Prawidłowa odpowiedź. Zgadłeś za {Tries} razem.");
                }
                
            }
            Console.ReadKey();
        }

        static void GetAppInfo()
        {
            string appName = "[Zgadywanie liczby]";
            int appVersion = 1;
            string appAuthor = "Tomasz Dyda";

            Console.BackgroundColor = ConsoleColor.White;

            string info = $"[{appName}] Wersja: 0.0.{appVersion}, Autor: {appAuthor}";

            PrintColorMessage(ConsoleColor.Magenta, info);
        }

        static string GetUserName()
        {
            Console.WriteLine("Jak masz na imie?");
            string InputUserName = Console.ReadLine();
            return InputUserName;
        }

        static void GreetUser (string UserName)
        {
            string info = $"Powodzenia {UserName}, odganij liczbę...";
            PrintColorMessage(ConsoleColor.Blue, info);
        }

        static void PrintColorMessage (ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
    


    
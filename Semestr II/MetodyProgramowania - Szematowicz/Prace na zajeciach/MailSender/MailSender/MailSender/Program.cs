using System;
using System.Net.Mail;

namespace MailSender
{
    class Program
    {
        
        
            public static void Sender()
            {
                string nadawca, temat, tresc, wybor;
                Console.WriteLine("Kto wysyła maila? ");

                foreach (Nadawcy item in Enum.GetValues(typeof(Nadawcy))) { Console.WriteLine("{0} - {1}", (int)item, item); }
                {
                    nadawca = Console.ReadLine();
                    if (nadawca == "Dyrektor" || nadawca == "dyrektor" || nadawca == "1")
                    {
                        nadawca = "dyrektor@wp.pl";
                    }
                    else if (nadawca == "Kierownik" || nadawca == "kierownik" || nadawca == "2")
                    {
                        nadawca = "kierownik@wp.pl";
                    }
                    else if (nadawca == "Kadrowa" || nadawca == "kadrowa" || nadawca == "3")
                    {
                        nadawca = "kadrowa@wp.pl";
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Nie ma takiego pracownika");
                        Console.WriteLine("Wybierz ponownie: ");
                        Sender();
                        return;
                    }

                }
                Console.Clear();
                Console.WriteLine($"Piszesz jako: {nadawca}\n");
                Console.WriteLine("Temat wiadomości: ");
                temat = Console.ReadLine();
                Console.WriteLine("Treść wiadomości: ");
                tresc = Console.ReadLine();
                Console.WriteLine($"\nPodgląd wiadomość przed wysłeniem: \n\nNadawca: {nadawca} \n\nTemat: {temat} \nTreść: {tresc} ");
                Console.WriteLine("\nCzy chcesz wysłać? (t/n)");
                wybor = Console.ReadLine();
                if (wybor == "T" || wybor == "t")
                {
                    var group = new string[]
                { "jan.kowalski@example.com", "anna.nowak@example.com", "adam.nowakowski@example.com", "master.blaster@wp.pl" };

                    var mail = new MailMessage(nadawca, "x@x", temat, tresc);

                    foreach (var recipient in group)
                    {
                        Console.WriteLine($"\nWysłano wiadomość od <{nadawca}> do: <{recipient}> o treści:\n\n{mail.Subject}\n{mail.Body} ");

                    }
                }
                else
                {
                    Console.Clear();
                    Sender();
                    return;
                }
            }
        
    

    static void Main(string[] args)
        {
            Sender();
            return;
            
        }
    }
}


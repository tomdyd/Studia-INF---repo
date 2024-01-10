using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public class Wysylacz
    {
        public void Sender()
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
                else if (nadawca == "Kadrowa" || nadawca == "kadrowa" || nadawca == "2")
                {
                    nadawca = "kadrowa@wp.pl";
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
            Console.WriteLine($"Piszesz jako: {nadawca}");
            Console.WriteLine("Temat wiadomości: ");
            temat = Console.ReadLine();
            Console.WriteLine("Treść wiadomości: ");
            tresc = Console.ReadLine();
            Console.WriteLine($"Wiadomość przed wysłeniem: \nNadawca: {nadawca}, \nTemat: {temat}, \nTreść: {tresc} ");
            Console.WriteLine("Czy chcesz wysłać? (t/n)");
            wybor = Console.ReadLine();
            if (wybor == "T" || wybor == "t")
            {
                var group = new string[]
            { "jan.kowalski@example.com", "anna.nowak@example.com", "adam.nowakowski@example.com", "master.blaster@wp.pl" };

                var mail = new MailMessage(nadawca, "x@x", temat, tresc);

                foreach (var recipient in group)
                {
                    Console.WriteLine($"\nMail został wysłany od {nadawca} do: {recipient}\n\n{mail.Subject}\n{mail.Body}");

                }
            }
            else
            {
                Console.Clear();
                Sender();
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mailSender
{
    public struct MS
    {
        public void Send(string sFrom, string sTo, string msg)
        {
            Console.WriteLine($"Wysyłam maila od {sFrom} do {sTo}. Treść wiadomości: {msg}");
        }
    }
    public struct Sekretariat
    {
        public MS _ms;

        public Sekretariat(MS mS)
        {
            _ms = mS;
        }

        public void Send(Pracownik from, Pracownik[] To, string msg)
        {
            foreach (Pracownik pracownik in To)
            {
                _ms.Send(from._email, pracownik._email, msg);
            }
        }
    }
}


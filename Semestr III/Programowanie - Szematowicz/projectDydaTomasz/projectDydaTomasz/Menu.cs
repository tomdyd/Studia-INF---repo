using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectDydaTomasz
{
    public class Menu : IMenu
    {
        public void MainMenu()
        {
            Console.WriteLine("1. Dodaj użytkownika");
            Console.WriteLine("2. Dodaj test");
            Console.WriteLine("3. Wczytaj użytkowników");
            Console.WriteLine("4. Wczytaj test");
            Console.WriteLine("5. Wyjdź");
        }
    }
}

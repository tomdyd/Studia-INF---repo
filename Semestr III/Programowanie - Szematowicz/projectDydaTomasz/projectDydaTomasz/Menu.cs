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
            Console.WriteLine("1. Połącz z MongoDb");
            Console.WriteLine("2. Połącz z sql");
            Console.WriteLine("3. Wyjdź");
        }

        public void CollectionsMenu()
        {
            Console.WriteLine("1. Samochody");
            Console.WriteLine("2. Mieszkania");
            Console.WriteLine("3. Wyloguj");
        }

        public void carMenu()
        {
            Console.WriteLine("1. Dodaj nowy samochód");
            Console.WriteLine("2. Wczytaj swoje samochody");
            Console.WriteLine("3. Zaktualizuj swój samochód");
            Console.WriteLine("4. Usuń swój samochód");
            Console.WriteLine("5. Wróć");
        }

        public void apartmentMenu()
        {
            Console.WriteLine("1. Dodaj nowe mieszkanie");
            Console.WriteLine("2. Wczytaj swoje mieszkania");
            Console.WriteLine("3. Zaktualizuj swój mieszkanie");
            Console.WriteLine("4. Usuń swój mieszkanie");
            Console.WriteLine("5. Wróć");
        }
    }
}

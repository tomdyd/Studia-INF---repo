using connectToMongoDbRawMaterials.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectToMongoDbRawMaterials
{
    public class Menu : IMenu
    {
        public void mainMenu()
        {
            Console.WriteLine("1. Dodaj surowiec chemiczny");
            Console.WriteLine("2. Pobierz listę surowców chemicznych");
            Console.WriteLine("3. Edytuj surowiec chemiczny");
            Console.WriteLine("4. Usuń surowiec chemiczny");
            Console.WriteLine("5. Wyjdź z programu");
        }
    }
}

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
    }
}

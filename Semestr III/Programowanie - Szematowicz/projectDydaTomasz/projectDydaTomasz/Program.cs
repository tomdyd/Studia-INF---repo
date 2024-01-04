
using MongoDB.Driver;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore.Models;

namespace projectDydaTomasz
{
    class Program
    {
        static void Main(string[] args)
        {
            IMenu menu = new Menu();
            IAppConsole console = new AppConsole();

            var appRunner = new AppRunner(menu, console);
            appRunner.StartApp();
        }
    }
}
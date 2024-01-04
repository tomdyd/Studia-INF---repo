
using MongoDB.Driver;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using projectDydaTomaszCore.Services;

namespace projectDydaTomasz
{
    class Program
    {
        static void Main(string[] args)
        {
            IMenu menu = new Menu();
            IAppConsole console = new AppConsole();
            IDatabaseConnection<User> userMongoClient = new MongoDbDatabaseConnection<User>();
            IDatabaseConnection<test> testMongoClient = new MongoDbDatabaseConnection<test>();
            IDataService<User> userService = new DataService<User>(userMongoClient);
            IDataService<test> testService = new DataService<test>(testMongoClient);

            var appRunner = new AppRunner(menu, console, userService, testService, userMongoClient, testMongoClient);
            appRunner.StartApp();
        }
    }
}
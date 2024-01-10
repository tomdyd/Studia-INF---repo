using projectDydaTomasz.Core;
using projectDydaTomasz.Core.Interfaces;
using projectDydaTomasz.Core.Models;
using projectDydaTomasz.Core.Services;
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
            IDatabaseConnection<Car> carMongoClient = new MongoDbDatabaseConnection<Car>();
            IDatabaseConnection<User> userSqlClient = new SqlitedatabaseConnection<User>();
            IUserService userMongoService = new UserService(userMongoClient);
            IUserService userSqlService = new UserService(userSqlClient);
            ICarService carService = new CarService(carMongoClient);

            var appRunner = new AppRunner(menu, console, userMongoClient, carMongoClient, userSqlClient, userMongoService, carService, userSqlService);
            appRunner.StartApp();
        }
    }
}
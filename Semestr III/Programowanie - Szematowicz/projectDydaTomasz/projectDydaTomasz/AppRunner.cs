using MongoDB.Driver.Core.Authentication;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore;
using projectDydaTomaszCore.Models;

namespace projectDydaTomasz
{
    public class AppRunner
    {
        private readonly IMenu _menu;
        private readonly IAppConsole _console;

        public AppRunner(IMenu menu, IAppConsole console)
        {
            _menu = menu;
            _console = console;
        }

        public void StartApp()
        {
            while (true)
            {
                _console.Clear();
                _menu.MainMenu();
                var res = _console.GetResponseFromUser();

                switch (res)
                {
                    case 1:
                        User newUser = new User
                        {
                            Username = "testUser",
                            PasswordHash = "testPassword",
                            Email = "testEmail"
                        };

                        var MongoDbDatabase = new MongoDbDatabaseConnection<User>();
                        MongoDbDatabase.Connect("mongodb://localhost:27017", "test", "user");
                        MongoDbDatabase.AddToDb(newUser);
                        _console.ReadLine();
                        break;
                    case 2:
                        test newTest = new test
                        {
                            MyNum = 1,
                            Name = "Tomek",
                        };

                        var MongoDbDatabaseTest = new MongoDbDatabaseConnection<test>();
                        MongoDbDatabaseTest.Connect("mongodb://localhost:27017", "test", "test");
                        MongoDbDatabaseTest.AddToDb(newTest);

                        _console.ReadLine();
                        break;
                    case 3:
                        MongoDbDatabase = new MongoDbDatabaseConnection<User>();
                        MongoDbDatabase.Connect("mongodb://localhost:27017", "test", "user");
                        var result = MongoDbDatabase.ReadFromDb();

                        foreach (User user in result)
                        {
                            Console.WriteLine(user);
                            Console.WriteLine();
                        }
                        Console.ReadLine();

                        break;

                    case 4:
                        MongoDbDatabaseTest = new MongoDbDatabaseConnection<test>();
                        MongoDbDatabaseTest.Connect("mongodb://localhost:27017", "test", "test");
                        var result1 = MongoDbDatabaseTest.ReadFromDb();

                        foreach (test test in result1)
                        {
                            Console.WriteLine(test);
                            Console.WriteLine();
                        }
                        Console.ReadLine();
                        break;

                    case 5:

                        return;

                    default:
                        Console.WriteLine("Nie ma takiej opcji");
                        _console.ReadLine();
                        break;
                }
            }
        }
    }
}

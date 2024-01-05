using projectDydaTomasz.Core.Models;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using projectDydaTomaszCore.Services;

namespace projectDydaTomasz
{
    public class AppRunner
    {
        private readonly IMenu _menu;
        private readonly IAppConsole _console;
        private readonly IUserService _userService;
        private readonly IDatabaseConnection<User> _userMongoClient;
        private readonly IDatabaseConnection<Car> _carMongoClient;

        public AppRunner(IMenu menu, IAppConsole console, IUserService userService, IDatabaseConnection<User> userMongoClient, IDatabaseConnection<Car> carMongoClient)
        {
            _menu = menu;
            _console = console;
            _userService = userService;
            _userMongoClient = userMongoClient;
            _carMongoClient = carMongoClient;
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
                        _userMongoClient.Connect("mongodb://localhost:27017/", "test", "user");
                        var login = _console.GetLoginFromUser();
                        var password = _console.GetPasswordFromUser();
                        var loggedUser = CheckUser(login, password);

                        if (loggedUser != null)
                        {
                            _console.WriteLine("Zalogowano poprawnie!");
                        }
                        else
                        {
                            _console.WriteLine("Niepoprawne dane!");
                        }
                        _console.ReadLine();

                        _console.Clear();
                        _menu.mongoCollectionsMenu();
                        res = _console.GetResponseFromUser();

                        switch (res)
                        {
                            case 1:
                                _carMongoClient.Connect("mongodb://localhost:27017/", "test", "car");
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }

                        break;

                    case 2:
                        _userMongoClient.Connect("mongodb://localhost:27017/", "test", "user");
                        //ShowAllUsers("test1");
                        break;
                    case 3:

                        return;

                    default:
                        _console.WriteLine("Nie ma takiej opcji");
                        _console.ReadLine();
                        break;
                }
            }
        }

        //private void ShowAllUsers(string username)
        //{
        //    var user = _userService.GetUser(username);
        //    Console.WriteLine(username);
        //    Console.WriteLine();
        //    _console.ReadLine();
        //}

        private User CheckUser(string username, string password)
        {
            User loggedUser = _userService.GetUser(username, password);
            return loggedUser;
        }
    }
}

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
        private readonly IDataService<User> _userService;
        private readonly IDataService<Test> _testService;
        private readonly IDatabaseConnection<User> _userMongoClient;
        private readonly IDatabaseConnection<Test> _testMongoClient;

        public AppRunner(IMenu menu, IAppConsole console, IDataService<User> userService, IDataService<Test> testService, IDatabaseConnection<User> userMongoClient, IDatabaseConnection<Test> testMongoClient)
        {
            _menu = menu;
            _console = console;
            _userService = userService;
            _testService = testService;
            _userMongoClient = userMongoClient;
            _testMongoClient = testMongoClient;
        }

        public void StartApp()
        {
            while (true)
            {
                _console.Clear();
                _menu.MenuM();
                var res = _console.GetResponseFromUser();

                switch (res)
                {
                    case 1:
                        _userMongoClient.Connect("mongodb://localhost:27017/", "test", "user");
                        ShowAllUsers();

                        Console.ReadLine();
                        break;
                    case 2:
                        _testMongoClient.Connect("mongodb://localhost:27017/", "test", "test");
                        ShowAllTests();
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

        private void ShowAllUsers()
        {
            var users = _userService.GetAllData();
            foreach (var item in users)
            {
                Console.WriteLine(item);
            }
            _console.ReadLine();
        }

        private void ShowAllTests()
        {
            var tests = _testService.GetAllData();
            foreach (var item in tests)
            {
                Console.WriteLine(item);
            }
            _console.ReadLine();
        }
    }
}

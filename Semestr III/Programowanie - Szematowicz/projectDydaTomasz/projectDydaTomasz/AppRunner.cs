using projectDydaTomasz.Core.Interfaces;
using projectDydaTomasz.Core.Models;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;

namespace projectDydaTomasz
{
    public class AppRunner
    {
        private readonly IMenu _menu;
        private readonly IAppConsole _console;
        private readonly IDatabaseConnection<User> _userMongoClient;
        private readonly IDatabaseConnection<Car> _carMongoClient;
        private readonly IUserService _userService;
        private readonly ICarService _carService;

        public AppRunner(IMenu menu, IAppConsole console, IDatabaseConnection<User> userMongoClient, IDatabaseConnection<Car> carMongoClient, IUserService userService, ICarService carService)
        {
            _menu = menu;
            _console = console;
            _userMongoClient = userMongoClient;
            _carMongoClient = carMongoClient;
            _userService = userService;
            _carService = carService;
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
                        var loggedUser = _userService.AuthorizeUser(login, password);

                        if (loggedUser != null)
                        {
                            bool runMongoCollectionsMenu = true;
                            while (runMongoCollectionsMenu)
                            {
                                _console.Clear();
                                _menu.CollectionsMenu();
                                res = _console.GetResponseFromUser();

                                switch (res)
                                {
                                    case 1:
                                        _carMongoClient.Connect("mongodb://localhost:27017/", "test", "car");
                                        bool runCarMenu = true;

                                        while (runCarMenu)
                                        {
                                            _console.Clear();
                                            _menu.carMenu();
                                            res = _console.GetResponseFromUser();

                                            switch (res)
                                            {
                                                case 1:
                                                    var numberOfCars = _carService.GetAllCarsList();
                                                    var newCar = new Car()
                                                    {
                                                        carNumber = numberOfCars.Count() + 1,
                                                        carBrand = _console.GetDataFromUser("Podaj markę samochodu: "),
                                                        carModel = _console.GetDataFromUser("Podaj model samochodu: "),
                                                        carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: "),
                                                        engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: "),
                                                        user = loggedUser
                                                    };

                                                    _carMongoClient.AddToDb(newCar);

                                                    break;
                                                case 2:
                                                    var searchTerm = _console.GetDataFromUser("Podaj model szukanego samochodu: ");
                                                    var carList = _carService.GetCars(loggedUser.userId);
                                                    foreach (var car in carList)
                                                    {
                                                        if (car.carModel.ToLower() == searchTerm.ToLower())
                                                        {
                                                            _console.WriteLine(
                                                                $"{car.carNumber}." +
                                                                $" marka: {car.carBrand}," +
                                                                $" model: {car.carModel}," +
                                                                $" rok produkcji: {car.carProductionYear}, " +
                                                                $"pojemność silnika: {car.engineCapacity}");
                                                        }
                                                    }
                                                    _console.ReadLine();
                                                    break;
                                                case 3:
                                                    bool isNumber = false;

                                                    do
                                                    {
                                                        searchTerm = _console.GetDataFromUser("Podaj nr samochodu który chcesz zaktualizować");
                                                        isNumber = int.TryParse(searchTerm, out int carNumber);
                                                    }while (!isNumber);


                                                    break;
                                                case 4:
                                                    break;
                                                case 5:
                                                    runCarMenu = false;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 2:
                                        _carMongoClient.Connect("mongodb://localhost:27017/", "test", "car");
                                        bool runApartmentMenu = true;

                                        while (runApartmentMenu)
                                        {
                                            _console.Clear();
                                            _menu.apartmentMenu();
                                            res = _console.GetResponseFromUser();

                                            switch (res)
                                            {
                                                case 1:
                                                    break;
                                                case 2:
                                                    break;
                                                case 3:
                                                    break;
                                                case 4:
                                                    break;
                                                case 5:
                                                    runApartmentMenu = false;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 3:
                                        runMongoCollectionsMenu = false;
                                        break;
                                    default:
                                        Console.WriteLine("Nie ma takiej opcji");
                                        break;
                                }
                            }
                        }
                        break;

                    case 2:
                        _userMongoClient.Connect("mongodb://localhost:27017/", "test", "user");
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

        
    }
}

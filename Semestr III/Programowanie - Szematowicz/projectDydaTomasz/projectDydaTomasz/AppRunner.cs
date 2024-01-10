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
        private readonly IDatabaseConnectionExtended<User> _userMongoClient;
        private readonly IDatabaseConnectionExtended<Car> _carMongoClient;
        private readonly IDatabaseConnection<User> _userSqlClient;
        private readonly IUserService _userMongoService;
        private readonly ICarService _carService;
        private readonly IUserService _userSqlService;

        public AppRunner(
            IMenu menu,
            IAppConsole console,
            IDatabaseConnectionExtended<User> userMongoClient,
            IDatabaseConnectionExtended<Car> carMongoClient,
            IDatabaseConnection<User> userSqlClient,
            IUserService userMongoService,
            ICarService carService,
            IUserService userSqlService)
        {
            _menu = menu;
            _console = console;
            _userMongoClient = userMongoClient;
            _carMongoClient = carMongoClient;
            _userSqlClient = userSqlClient;
            _userMongoService = userMongoService;
            _carService = carService;
            _userSqlService = userSqlService;
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

                        var runLoginMenu = true;
                        while (runLoginMenu)
                        {
                            _console.Clear();
                            _menu.LoginMenu();
                            res = _console.GetResponseFromUser();
                            switch (res)
                            {
                                case 1:
                                    _userMongoClient.Connect("mongodb://localhost:27017/", "test", "user");
                                    var login = _console.GetLoginFromUser();
                                    var password = _console.GetPasswordFromUser();
                                    var loggedUser = _userMongoService.AuthorizeUser(login, password);

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
                                                                var numberOfCars = _carService.GetCars(loggedUser.userId);
                                                                var newCar = CreateCar(numberOfCars, loggedUser);
                                                                _carMongoClient.AddToDb(newCar);

                                                                break;

                                                            case 2:
                                                                var carList = _carService.GetCars(loggedUser.userId);
                                                                foreach (var car in carList)
                                                                {
                                                                    _console.WriteLine(
                                                                        $"{car.carNumber}." +
                                                                        $" Marka: {car.carBrand}," +
                                                                        $" model: {car.carModel}," +
                                                                        $" rok produkcji: {car.carProductionYear}, " +
                                                                        $"pojemność silnika: {car.engineCapacity}");
                                                                }
                                                                _console.ReadLine();
                                                                break;

                                                            case 3:
                                                                var searchTerm = _console.GetDataFromUser("Podaj marke szukanego samochodu: ");
                                                                carList = _carService.GetCars(loggedUser.userId);
                                                                foreach (var car in carList)
                                                                {
                                                                    if (car.carBrand.ToLower() == searchTerm.ToLower())
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{car.carNumber}." +
                                                                            $" Marka: {car.carBrand}," +
                                                                            $" model: {car.carModel}," +
                                                                            $" rok produkcji: {car.carProductionYear}, " +
                                                                            $"pojemność silnika: {car.engineCapacity}");
                                                                    }
                                                                }
                                                                _console.ReadLine();
                                                                break;
                                                            case 4:
                                                                _console.Write("Podaj numer samochodu który chcesz zaktualizować: ");
                                                                var carNumber = _console.GetResponseFromUser();
                                                                carList = _carService.GetCars(loggedUser.userId);

                                                                var updatingCar = carList.Find(x => x.carNumber == carNumber);
                                                                if (updatingCar != null)
                                                                {
                                                                    updatingCar.carId = updatingCar.carId;
                                                                    updatingCar.carNumber = updatingCar.carNumber;
                                                                    updatingCar.carBrand = _console.GetDataFromUser("Podaj markę samochodu: ");
                                                                    updatingCar.carModel = _console.GetDataFromUser("Podaj model samochodu: ");
                                                                    updatingCar.carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: ");
                                                                    updatingCar.engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: ");
                                                                    updatingCar.user = loggedUser;

                                                                    _carService.UpdateCar(updatingCar);

                                                                    _console.WriteLine("Dane zaktualizowane!");
                                                                    _console.ReadLine();
                                                                }
                                                                else
                                                                {
                                                                    _console.WriteLine("Nie znaleziono samochodu!");
                                                                    _console.ReadLine();
                                                                }
                                                                break;
                                                            case 5:
                                                                _console.Write("Podaj numer samochodu który chcesz usunąć: ");
                                                                carNumber = _console.GetResponseFromUser();
                                                                carList = _carService.GetCars(loggedUser.userId);

                                                                var deletingCar = carList.Find(x => x.carNumber == carNumber);
                                                                if (deletingCar != null)
                                                                {
                                                                    _carService.DeleteCar(deletingCar.carId);
                                                                    _console.WriteLine("Samochód został usunięty!");
                                                                }
                                                                else
                                                                {
                                                                    _console.WriteLine("Nie znaleziono samochodu!");
                                                                    _console.ReadLine();
                                                                }

                                                                break;
                                                            case 6:
                                                                runCarMenu = false;
                                                                break;

                                                            default:
                                                                Console.WriteLine("Nie ma takiej opcji");
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
                                    var newUser = new User()
                                    {
                                        username = _console.GetDataFromUser("Podaj login: "),
                                        passwordHash = _console.GetPasswordFromUser(),
                                        email = _console.GetDataFromUser("Podaj adres email: ")
                                    };
                                    _userMongoClient.Connect("mongodb://localhost:27017/", "test", "user");
                                    _userMongoService.RegisterUser(newUser);

                                    break;

                                case 3:
                                    runLoginMenu = false;
                                    break;

                                default:
                                    _console.WriteLine("Nie ma takiej opcji!");
                                    break;
                            }

                        }
                        break;

                    case 2:

                        runLoginMenu = true;
                        while (runLoginMenu)
                        {
                            _console.Clear();
                            _menu.LoginMenu();
                            res = _console.GetResponseFromUser();
                            switch (res)
                            {
                                case 1:
                                    //logowanie
                                    break;

                                case 2:
                                    var sqlUser = new User()
                                    {
                                        username = _console.GetDataFromUser("Podaj login: "),
                                        passwordHash = _console.GetPasswordFromUser(),
                                        email = _console.GetDataFromUser("Podaj adres email: ")
                                    };

                                    _userSqlService.RegisterUser(sqlUser);
                                    break;

                                case 3:
                                    runLoginMenu = false;
                                    break;

                                default:
                                    _console.WriteLine("Nie ma takiej opcji!");
                                    break;
                            }
                        }

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

        private Car CreateCar(List<Car> numberOfCars, User loggedUser)
        {
            var newCar = new Car()
            {
                carNumber = numberOfCars.Count() + 1,
                carBrand = _console.GetDataFromUser("Podaj markę samochodu: "),
                carModel = _console.GetDataFromUser("Podaj model samochodu: "),
                carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: "),
                engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: "),
                user = loggedUser
            };

            return newCar;
        }
    }
}

using projectDydaTomasz.Core.Interfaces;
using projectDydaTomasz.Core.Models;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using System.Diagnostics;
using ZstdSharp.Unsafe;

namespace projectDydaTomasz
{
    public class AppRunner
    {
        private readonly IMenu _menu;
        private readonly IAppConsole _console;
        private readonly IDatabaseConnectionExtended<User> _userMongoClient;
        private readonly IDatabaseConnectionExtended<Car> _carMongoClient;
        private readonly IUserService _userMongoService;
        private readonly ICarService _carMongoService;
        private readonly IApartmentService _apartmentMongoService;
        private readonly IUserService _userSqlService;
        private readonly ICarService _carSqlService;
        private readonly IApartmentService? _apartmentSqlService;

        public AppRunner(
            IMenu menu,
            IAppConsole console,
            IDatabaseConnectionExtended<User> userMongoClient,
            IDatabaseConnectionExtended<Car> carMongoClient,
            IUserService userMongoService,
            ICarService carMongoService,
            IApartmentService apartmentMongoService,
            IUserService userSqlService,
            ICarService carSqlService,
            IApartmentService apartmentSqlService)
        {
            _menu = menu;
            _console = console;
            _userMongoClient = userMongoClient;
            _carMongoClient = carMongoClient;
            _userMongoService = userMongoService;
            _carMongoService = carMongoService;
            _apartmentMongoService = apartmentMongoService;
            _userSqlService = userSqlService;
            _carSqlService = carSqlService;
            _apartmentSqlService = apartmentSqlService;
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
                                        bool runCollectionsMenu = true;
                                        while (runCollectionsMenu)
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
                                                                var numberOfCars = _carMongoService.GetCars("user", loggedUser.userId);
                                                                var newCar = new Car()
                                                                {
                                                                    carBrand = _console.GetDataFromUser("Podaj markę samochodu: "),
                                                                    carModel = _console.GetDataFromUser("Podaj model samochodu: "),
                                                                    carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: "),
                                                                    engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: "),
                                                                    user = loggedUser.userId
                                                                };
                                                                _carMongoService.CreateCar(newCar);
                                                                _console.ReadLine();


                                                                break;

                                                            case 2:
                                                                var carList = _carMongoService.GetCars("user", loggedUser.userId);
                                                                for (int i = 0; i < carList.Count; i++)
                                                                {
                                                                    _console.WriteLine(
                                                                        $"{i + 1}." +
                                                                        $" Marka: {carList[i].carBrand}," +
                                                                        $" model: {carList[i].carModel}," +
                                                                        $" rok produkcji: {carList[i].carProductionYear}," +
                                                                        $" pojemność silnika: {carList[i].engineCapacity}");
                                                                }
                                                                _console.ReadLine();
                                                                break;

                                                            case 3:
                                                                var searchTerm = _console.GetDataFromUser("Podaj marke szukanego samochodu: ");
                                                                carList = _carMongoService.GetCars("userId", loggedUser.userId);
                                                                for (int i = 0; i < carList.Count; i++)
                                                                {
                                                                    _console.WriteLine(
                                                                        $"{i + 1}." +
                                                                        $" Marka: {carList[i].carBrand}," +
                                                                        $" model: {carList[i].carModel}," +
                                                                        $" rok produkcji: {carList[i].carProductionYear}," +
                                                                        $" pojemność silnika: {carList[i].engineCapacity}");
                                                                }
                                                                _console.ReadLine();
                                                                break;

                                                            case 4:
                                                                _console.Write("Podaj numer samochodu który chcesz zaktualizować: ");
                                                                var carNumber = _console.GetResponseFromUser();
                                                                carList = _carMongoService.GetCars("userId", loggedUser.userId);

                                                                if (carNumber <= carList.Count)
                                                                {
                                                                    var updatingCar = carList[carNumber];

                                                                    updatingCar.carId = updatingCar.carId;
                                                                    updatingCar.carBrand = _console.GetDataFromUser("Podaj markę samochodu: ");
                                                                    updatingCar.carModel = _console.GetDataFromUser("Podaj model samochodu: ");
                                                                    updatingCar.carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: ");
                                                                    updatingCar.engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: ");
                                                                    updatingCar.user = loggedUser.userId;

                                                                    _carMongoService.UpdateCar(updatingCar);

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
                                                                carList = _carMongoService.GetCars("user", loggedUser.userId);

                                                                if (carNumber <= carList.Count)
                                                                {
                                                                    var deletingCar = carList[carNumber];

                                                                    _carMongoService.DeleteCar(deletingCar.carId);
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
                                                    runCollectionsMenu = false;
                                                    loggedUser = null;
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
                                    var login = _console.GetLoginFromUser();
                                    var password = _console.GetPasswordFromUser();
                                    var loggedUser = _userSqlService.AuthorizeUser(login, password);

                                    if (loggedUser != null)
                                    {
                                        bool runCollectionsMenu = true;

                                        while (runCollectionsMenu)
                                        {
                                            _console.Clear();
                                            _menu.CollectionsMenu();
                                            res = _console.GetResponseFromUser();

                                            switch (res)
                                            {
                                                case 1:
                                                    bool runCarMenu = true;

                                                    while (runCarMenu)
                                                    {
                                                        _console.Clear();
                                                        _menu.carMenu();
                                                        res = _console.GetResponseFromUser();

                                                        switch (res)
                                                        {
                                                            case 1:
                                                                try
                                                                {
                                                                    var numberOfCars = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");

                                                                    var newCar = new Car()
                                                                    {
                                                                        carBrand = _console.GetDataFromUser("Podaj markę samochodu: "),
                                                                        carModel = _console.GetDataFromUser("Podaj model samochodu: "),
                                                                        carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: "),
                                                                        engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: "),
                                                                        user = loggedUser.userId
                                                                    };

                                                                    _carSqlService.CreateCar(newCar);
                                                                    _console.WriteLine("Dodano do bazy danych!");
                                                                    _console.ReadLine();
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }

                                                                break;

                                                            case 2:
                                                                try
                                                                {
                                                                    var carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");
                                                                    for (int i = 0; i < carList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i + 1}." +
                                                                            $" Marka: {carList[i].carBrand}," +
                                                                            $" model: {carList[i].carModel}," +
                                                                            $" rok produkcji: {carList[i].carProductionYear}," +
                                                                            $" pojemność silnika: {carList[i].engineCapacity}");
                                                                    }
                                                                    _console.ReadLine();
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }
                                                                break;

                                                            case 3:
                                                                try
                                                                {
                                                                    var searchTerm = _console.GetDataFromUser("Podaj marke szukanego samochodu: ");
                                                                    var carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE carBrand = '{searchTerm}'");
                                                                    for (int i = 0; i < carList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i + 1}." +
                                                                            $" Marka: {carList[i].carBrand}," +
                                                                            $" model: {carList[i].carModel}," +
                                                                            $" rok produkcji: {carList[i].carProductionYear}," +
                                                                            $" pojemność silnika: {carList[i].engineCapacity}");
                                                                    }

                                                                    _console.ReadLine();
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }

                                                                break;

                                                            case 4:
                                                                try
                                                                {
                                                                    var carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");

                                                                    for (int i = 0; i < carList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i+1}." +
                                                                            $" Marka: {carList[i].carBrand}," +
                                                                            $" model: {carList[i].carModel}," +
                                                                            $" rok produkcji: {carList[i].carProductionYear}," +
                                                                            $" pojemność silnika: {carList[i].engineCapacity}");
                                                                    }
                                                                    //foreach (var car in carList)
                                                                    //{
                                                                    //    _console.WriteLine(
                                                                    //        $" Marka: {car.carBrand}," +
                                                                    //        $" model: {car.carModel}," +
                                                                    //        $" rok produkcji: {car.carProductionYear}," +
                                                                    //        $" pojemność silnika: {car.engineCapacity}");
                                                                    //}

                                                                    _console.Write("Podaj numer samochodu który chcesz zaktualizować: ");
                                                                    var carNumber = _console.GetResponseFromUser();
                                                                    carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");

                                                                    if (carNumber <= carList.Count)
                                                                    {

                                                                        var updatingCar = carList[carNumber - 1];
                                                                        updatingCar.carId = updatingCar.carId;
                                                                        updatingCar.carBrand = _console.GetDataFromUser("Podaj markę samochodu: ");
                                                                        updatingCar.carModel = _console.GetDataFromUser("Podaj model samochodu: ");
                                                                        updatingCar.carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: ");
                                                                        updatingCar.engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: ");

                                                                        _carSqlService.UpdateCar(updatingCar);

                                                                        _console.WriteLine("Dane zaktualizowane!");
                                                                        _console.ReadLine();
                                                                    }
                                                                    else
                                                                    {
                                                                        _console.WriteLine("Nie znaleziono samochodu!");
                                                                        _console.ReadLine();
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }

                                                                break;

                                                            case 5:
                                                                try
                                                                {
                                                                    var carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");
                                                                    for (int i = 0; i < carList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i + 1}." +
                                                                            $" Marka: {carList[i].carBrand}," +
                                                                            $" model: {carList[i].carModel}," +
                                                                            $" rok produkcji: {carList[i].carProductionYear}," +
                                                                            $" pojemność silnika: {carList[i].engineCapacity}");
                                                                    }

                                                                    _console.Write("Podaj numer samochodu który chcesz usunąć: ");
                                                                    var carNumber = _console.GetResponseFromUser();
                                                                    carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");

                                                                    if (carNumber <= carList.Count)
                                                                    {
                                                                        var deletingCar = carList[carNumber];

                                                                        _carSqlService.DeleteCar(deletingCar.carId);
                                                                        _console.WriteLine("Samochód został usunięty!");
                                                                        _console.ReadLine();
                                                                    }
                                                                    else
                                                                    {
                                                                        _console.WriteLine("Nie znaleziono samochodu!");
                                                                        _console.ReadLine();
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }

                                                                break;

                                                            case 6:
                                                                runCarMenu = false;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    try
                                                    {
                                                        var numberOfApartments = _apartmentSqlService.GetApartments("SELECT * FROM Apartments", $"WHERE user = '{loggedUser.userId}'");

                                                        var newApartment = new Apartment()
                                                        {
                                                            apartmentNumber = numberOfApartments.Count + 1,
                                                            surface = _console.GetDataFromUser("Podaj powierzchnię mieszkania: "),
                                                            cost = _console.GetDataFromUser("Podaj cenę mieszkania: "),
                                                            street = _console.GetDataFromUser("Podaj adres mieszkania: "),
                                                            user = loggedUser.userId
                                                        };

                                                        _apartmentSqlService.CreateApartment(newApartment);
                                                        _console.WriteLine("Dodano do bazy danych!");
                                                        _console.ReadLine();
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        _console.WriteLine(e.Message);
                                                        _console.ReadLine();
                                                    }
                                                    break;
                                                case 3:
                                                    runCollectionsMenu = false;
                                                    loggedUser = null;
                                                    break;
                                                case 4:
                                                    break;
                                            }
                                        }
                                    }
                                    break;

                                case 2:
                                    var sqlUser = new User()
                                    {
                                        userId = "004371db-c3c3-49ab-a9-64c10592d41718f7",
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
    }
}

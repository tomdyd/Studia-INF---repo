﻿using projectDydaTomasz.Core.Interfaces;
using projectDydaTomasz.Core.Models;
using projectDydaTomasz.Core.Services;
using projectDydaTomasz.Interfaces;
using projectDydaTomaszCore.Models;

namespace projectDydaTomasz
{
    public class AppRunner
    {
        private readonly IMenu _menu;
        private readonly IAppConsole _console;
        private readonly IDatabaseConnectionExtended<User> _userMongoClient;
        private readonly IDatabaseConnectionExtended<Car> _carMongoClient;
        private readonly IDatabaseConnectionExtended<Apartment> _apartmentMongoClient;
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
            IDatabaseConnectionExtended<Apartment> apartmentMongoClient,
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
            _apartmentMongoClient = apartmentMongoClient;
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
                                    _console.Clear();
                                    _userMongoClient.Connect("mongodb://localhost:27017/", "dataBase", "user");
                                    var login = _console.GetLoginFromUser();
                                    var password = _console.GetPasswordFromUser();
                                    var loggedUser = _userMongoService.AuthorizeUser(login, password);

                                    if (loggedUser != null)
                                    {
                                        bool runCollectionsMenu = true;

                                        MongoCollectionsMenu(runCollectionsMenu, loggedUser);
                                    }
                                    break;

                                case 2:
                                    var newUser = new User()
                                    {
                                        username = _console.GetDataFromUser("Podaj login: "),
                                        passwordHash = _console.GetPasswordFromUser(),
                                        email = _console.GetDataFromUser("Podaj adres email: ")
                                    };
                                    _userMongoClient.Connect("mongodb://localhost:27017/", "dataBase", "user");
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
                                                                CreateCar(loggedUser, _carSqlService);

                                                                break;

                                                            case 2:
                                                                var carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");
                                                                PrintList(carList);

                                                                break;

                                                            case 3:

                                                                carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");
                                                                PrintFilteredCarsList(carList);

                                                                break;

                                                            case 4:

                                                                carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");
                                                                UpdateCar(loggedUser, carList, _carSqlService);

                                                                break;

                                                            case 5:
                                                                carList = _carSqlService.GetCars("SELECT * FROM Cars", $"WHERE user = '{loggedUser.userId}'");
                                                                DeleteCar(carList, _carSqlService);

                                                                break;

                                                            case 6:
                                                                runCarMenu = false;
                                                                break;

                                                            default:
                                                                _console.WriteLine("Nie ma takiej opcji!");
                                                                _console.ReadLine();
                                                                break;
                                                        }
                                                    }
                                                    break; // wybór kolekcji samochodów w sqLite

                                                case 2:
                                                    bool runApartmentMenu = true;

                                                    while (runApartmentMenu)
                                                    {
                                                        _console.Clear();
                                                        _menu.apartmentMenu();
                                                        res = _console.GetResponseFromUser();

                                                        switch (res)
                                                        {
                                                            case 1:
                                                                try
                                                                {
                                                                    var newApartment = new Apartment()
                                                                    {
                                                                        surface = _console.GetDataFromUser("Podaj powierzchnię mieszkania: "),
                                                                        street = _console.GetDataFromUser("Podaj adres mieszkania: "),
                                                                        cost = _console.GetDataFromUser("Podaj cenę mieszkania: "),
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
                                                                break; // Dodawanie mnieszkań do bazy sqLite

                                                            case 2:
                                                                try
                                                                {
                                                                    var apartmentsList = _apartmentSqlService.GetApartments("SELECT * FROM Apartments", $"WHERE user = '{loggedUser.userId}'");
                                                                    for (int i = 0; i < apartmentsList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i + 1}." +
                                                                            $" Powierzchnia mieszkania: {apartmentsList[i].surface}," +
                                                                            $" Adres mieszkania: {apartmentsList[i].street}," +
                                                                            $" Cena mieszkania: {apartmentsList[i].cost}");
                                                                    }
                                                                    _console.ReadLine();
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }
                                                                break; // Wczytywanie mieszkań użytkownika z bazy sqLite

                                                            case 3:
                                                                try
                                                                {
                                                                    var apartmentsList = _apartmentSqlService.GetApartments("SELECT * FROM Apartments", $"WHERE user = '{loggedUser.userId}'");
                                                                    for (int i = 0; i < apartmentsList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i + 1}." +
                                                                            $" Powierzchnia mieszkania: {apartmentsList[i].surface}," +
                                                                            $" Adres mieszkania: {apartmentsList[i].street}," +
                                                                            $" Cena mieszkania: {apartmentsList[i].cost}");
                                                                    }

                                                                    _console.Write("Podaj numer mieszkania które chcesz zaktualizować: ");
                                                                    var apartmentNumber = _console.GetResponseFromUser();
                                                                    apartmentsList = _apartmentSqlService.GetApartments("SELECT * FROM Apartments", $"WHERE user = '{loggedUser.userId}'");

                                                                    if (apartmentNumber <= apartmentsList.Count)
                                                                    {
                                                                        var updatingApartment = apartmentsList[apartmentNumber - 1];

                                                                        updatingApartment.surface = _console.GetDataFromUser("Podaj powierzchnię mieszkania: ");
                                                                        updatingApartment.street = _console.GetDataFromUser("Podaj adres mieszkania: ");
                                                                        updatingApartment.cost = _console.GetDataFromUser("Podaj cenę mieszkania: ");
                                                                        updatingApartment.user = loggedUser.userId;

                                                                        _apartmentSqlService.UpdateApartment(updatingApartment);

                                                                        _console.WriteLine("Dane zaktualizowane!");
                                                                        _console.ReadLine();
                                                                    }
                                                                    else
                                                                    {
                                                                        _console.WriteLine("Nie znaleziono mieszkania!");
                                                                        _console.ReadLine();
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }
                                                                break; // Aktualizacja mieszkań użytkownika w bazie sqLite

                                                            case 4:
                                                                try
                                                                {
                                                                    var apartmentsList = _apartmentSqlService.GetApartments("SELECT * FROM Apartments", $"WHERE user = '{loggedUser.userId}'");
                                                                    for (int i = 0; i < apartmentsList.Count; i++)
                                                                    {
                                                                        _console.WriteLine(
                                                                            $"{i + 1}." +
                                                                            $" Powierzchnia mieszkania: {apartmentsList[i].surface}," +
                                                                            $" Adres mieszkania: {apartmentsList[i].street}," +
                                                                            $" Cena mieszkania: {apartmentsList[i].cost}");
                                                                    }

                                                                    _console.Write("Podaj numer mieszkania które chcesz usunąć: ");
                                                                    var apartmentNumber = _console.GetResponseFromUser();
                                                                    apartmentsList = _apartmentSqlService.GetApartments("SELECT * FROM Apartments", $"WHERE user = '{loggedUser.userId}'");

                                                                    if (apartmentNumber <= apartmentsList.Count)
                                                                    {
                                                                        var deletingApartment = apartmentsList[apartmentNumber - 1];

                                                                        _apartmentSqlService.DeleteApartment(deletingApartment.apartmentId);

                                                                        _console.WriteLine("Mieszkanie zostało usunięte!");
                                                                        _console.ReadLine();
                                                                    }
                                                                    else
                                                                    {
                                                                        _console.WriteLine("Nie znaleziono mieszkania!");
                                                                        _console.ReadLine();
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    _console.WriteLine(e.Message);
                                                                    _console.ReadLine();
                                                                }
                                                                break; // Usuwanie mieszkań użytkownika z bazy sqLite

                                                            case 5:
                                                                runApartmentMenu = false;
                                                                break; // Powrót

                                                            default:
                                                                _console.WriteLine("Nie ma takiej opcji!");
                                                                break;
                                                        }
                                                    }

                                                    break; // wybór kolekcji mieszkań w sqLite

                                                case 3:
                                                    runCollectionsMenu = false;
                                                    loggedUser = null;
                                                    break; // powrót

                                                default:
                                                    _console.WriteLine("Nie ma takiej opcji!");
                                                    _console.ReadLine();
                                                    break;
                                            }
                                        }
                                    }
                                    break; // logowanie

                                case 2:
                                    var sqlUser = new User()
                                    {
                                        userId = "004371db-c3c3-49ab-a9-64c10592d41718f7",
                                        username = _console.GetDataFromUser("Podaj login: "),
                                        passwordHash = _console.GetPasswordFromUser(),
                                        email = _console.GetDataFromUser("Podaj adres email: ")
                                    };

                                    _userSqlService.RegisterUser(sqlUser);
                                    break; // rejestracja

                                case 3:
                                    runLoginMenu = false;
                                    break; // powrót

                                default:
                                    _console.WriteLine("Nie ma takiej opcji!");
                                    _console.ReadLine();
                                    break;
                            }
                        }

                        break; //logowanie do sqlite

                    case 3:
                        return; //Wyjście

                    default:
                        _console.WriteLine("Nie ma takiej opcji");
                        _console.ReadLine();
                        break;
                }
            }
        }
        private void MongoCarMenu(bool runCarMenu, User loggedUser)
        {
            while (runCarMenu)
            {
                _console.Clear();
                _menu.carMenu();
                var res = _console.GetResponseFromUser();

                switch (res)
                {
                    case 1:

                        CreateCar(loggedUser, _carMongoService);
                        break;

                    case 2:
                        var carList = _carMongoService.GetCars("user", loggedUser.userId);
                        PrintList(carList);

                        break;

                    case 3:
                        var dataList = _carMongoService.GetCars("user", loggedUser.userId);
                        PrintFilteredCarsList(dataList);

                        break;

                    case 4:
                        carList = _carMongoService.GetCars("user", loggedUser.userId);
                        UpdateCar(loggedUser, carList, _carMongoService);

                        break;

                    case 5:
                        carList = _carMongoService.GetCars("user", loggedUser.userId);
                        DeleteCar(carList, _carMongoService);

                        break;

                    case 6:
                        runCarMenu = false;
                        break;

                    default:
                        _console.WriteLine("Nie ma takiej opcji");
                        break;
                }
            }
        }

        private void MongoApartmentMenu(bool  runApartmentMenu, User loggedUser)
        {
            while (runApartmentMenu)
            {
                _console.Clear();
                _menu.apartmentMenu();
                var res = _console.GetResponseFromUser();

                switch (res)
                {
                    case 1:
                       
                        CreateMongoApartment(loggedUser);

                        break;

                    case 2:
                        var apartmentsList = _apartmentMongoService.GetApartments("user", loggedUser.userId);
                        PrintList(apartmentsList);

                        break;

                    case 3:
                        
                        UpdateMongoApartment(loggedUser);

                        break; 

                    case 4:

                        DeleteMongoApartment(loggedUser);

                        break; 

                    case 5:
                        runApartmentMenu = false;
                        break; 

                    default:
                        _console.WriteLine("Nie ma takiej opcji!");
                        _console.ReadLine();
                        break;
                }
            }
        }

        private void MongoCollectionsMenu(bool runCollectionsMenu, User loggedUser)
        {
            while (runCollectionsMenu)
            {
                _console.Clear();
                _menu.CollectionsMenu();
                var res = _console.GetResponseFromUser();

                switch (res)
                {
                    case 1:
                        _carMongoClient.Connect("mongodb://localhost:27017/", "dataBase", "car");
                        bool runCarMenu = true;

                        MongoCarMenu(runCarMenu, loggedUser);

                        break; // wybór kolekcji samochodów

                    case 2:
                        _apartmentMongoClient.Connect("mongodb://localhost:27017/", "dataBase", "apartment");
                        bool runApartmentMenu = true;

                        MongoApartmentMenu(runApartmentMenu, loggedUser);

                        break; // wybór kolekcji mieszkań

                    case 3:
                        runCollectionsMenu = false;
                        loggedUser = null;
                        break; // powrót

                    default:
                        _console.WriteLine("Nie ma takiej opcji");
                        break;
                }
            }
        }

        private void Print<T>(List<T> dataList)
        {
            foreach (T item in dataList)
            {
                _console.Write($"{dataList.IndexOf(item)+1}. ");
                _console.WriteLine(item.ToString());
            }
        }

        private void DeleteCar(List<Car> carList, ICarService carService)
        {
            try
            {              
                Print(carList);

                _console.Write("Podaj numer samochodu który chcesz usunąć: ");
                var carNumber = _console.GetResponseFromUser();

                if (carNumber <= carList.Count)
                {
                    var deletingCar = carList[carNumber - 1];

                    carService.DeleteCar(deletingCar.carId);
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
        }

        private void DeleteMongoApartment(User loggedUser)
        {
            try
            {
                var apartmentsList = _apartmentMongoService.GetApartments("user", loggedUser.userId);

                Print(apartmentsList);

                _console.Write("Podaj numer mieszkania które chcesz usunąć: ");

                var apartmentNumber = _console.GetResponseFromUser();

                if (apartmentNumber <= apartmentsList.Count)
                {
                    var deletingApartment = apartmentsList[apartmentNumber - 1];

                    _apartmentMongoService.DeleteApartment(deletingApartment.apartmentId);

                    _console.WriteLine("Mieszkanie zostało usunięte!");
                    _console.ReadLine();
                }
                else
                {
                    _console.WriteLine("Nie znaleziono mieszkania!");
                    _console.ReadLine();
                }
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.ReadLine();
            }
        }

        private void CreateCar(User loggedUser, ICarService carService)
        {
            try
            {
                Car newCar = new Car();

                ReadCar(loggedUser, newCar);

                carService.CreateCar(newCar);
                _console.ReadLine();
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.ReadLine();
            };
        }

        private void CreateMongoApartment(User loggedUser)
        {
            try
            {
                var newApartment = new Apartment();

                ReadMongoApartment(loggedUser, newApartment);

                _apartmentMongoService.CreateApartment(newApartment);
                _console.WriteLine("Dodano do bazy danych!");
                _console.ReadLine();
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.ReadLine();
            };
        }

        private Car ReadCar(User loggedUser, Car car)
        {
            if(car.carId != null)
            {
                car.carId = car.carId;
            }
            car.carBrand = _console.GetDataFromUser("Podaj markę samochodu: ");
            car.carModel = _console.GetDataFromUser("Podaj model samochodu: ");
            car.carProductionYear = _console.GetDataFromUser("Podaj rok produkcji: ");
            car.engineCapacity = _console.GetDataFromUser("Podaj pojemność silnika: ");
            car.user = loggedUser.userId;

            return car;
        }

        private Apartment ReadMongoApartment(User loggedUser, Apartment apartment)
        {
            if(apartment.apartmentId != null)
            {
                apartment.apartmentId = apartment.apartmentId;
            }
            apartment.surface = _console.GetDataFromUser("Podaj powierzchnię mieszkania: ");
            apartment.street = _console.GetDataFromUser("Podaj adres mieszkania: ");
            apartment.cost = _console.GetDataFromUser("Podaj cenę mieszkania: ");
            apartment.user = loggedUser.userId;

            return apartment;
        }

        private void PrintList<T> (List<T> dataList)
        {
            try
            {               
                Print(dataList);

                _console.ReadLine();
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.ReadLine();
            }
        }

        private void PrintFilteredCarsList(List<Car> dataList)
        {
            try
            {
                var searchTerm = _console.GetDataFromUser("Podaj marke szukanego samochodu: ");
                List<Car> carList = new List<Car>();

                foreach(var item in dataList)
                {
                    if(item.carBrand == searchTerm)
                    {
                        carList.Add(item);
                    }
                }

                Print(carList);

                _console.ReadLine();
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.ReadLine();
            }
        }

        private void UpdateCar(User loggedUser, List<Car> carList, ICarService carService)
        {
            try
            {
                Print(carList);

                _console.Write("Podaj numer samochodu który chcesz zaktualizować: ");
                var carNumber = _console.GetResponseFromUser();

                if (carNumber <= carList.Count)
                {
                    var updatingCar = carList[carNumber - 1];

                    ReadCar(loggedUser, updatingCar);

                    carService.UpdateCar(updatingCar);

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
        }

        private void UpdateMongoApartment(User loggedUser)
        {
            try
            {
                var apartmentsList = _apartmentMongoService.GetApartments("user", loggedUser.userId);

                Print(apartmentsList);

                _console.Write("Podaj numer mieszkania które chcesz zaktualizować: ");
                var apartmentNumber = _console.GetResponseFromUser();

                if (apartmentNumber <= apartmentsList.Count)
                {
                    var updatingApartment = apartmentsList[apartmentNumber - 1];

                    ReadMongoApartment(loggedUser, updatingApartment);

                    _apartmentMongoService.UpdateApartment(updatingApartment);

                    _console.WriteLine("Dane zaktualizowane!");
                    _console.ReadLine();
                }
                else
                {
                    _console.WriteLine("Nie znaleziono mieszkania!");
                    _console.ReadLine();
                }
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.ReadLine();
            }
        }

    }
}

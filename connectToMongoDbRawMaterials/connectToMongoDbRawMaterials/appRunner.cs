using connectToMongoDbRawMaterials.Interfaces;
using connectToMongoDbRawMaterials.Models;
using ConnectToMongoDbRawMaterials.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace connectToMongoDbRawMaterials
{
    public class appRunner
    {
        private readonly IMongoClient<RawMaterial> _mongoRepository;
        private readonly IAppConsole _console;
        private readonly IMenu _menu;

        public appRunner(IMongoClient<RawMaterial> mongoRepository, IAppConsole console, IMenu menu)
        {
            _mongoRepository = mongoRepository;
            _console = console;
            _menu = menu;
        }

        public void startApp()
        {
            _mongoRepository.Connect();
            while (true)
            {
                _console.Clear();
                _menu.mainMenu();
                var res = _console.GetResponeFromUser();

                switch (res)
                {
                    case 1:

                        try
                        {
                            var name = _console.GetDataFromUser("Podaj nazwę surowca chemicznego: ");
                            var supplier = _console.GetDataFromUser("Podaj nazwę dostawcy surowca chemicznego: ");
                            DateOnly acceptanceDate = DateOnly.Parse(_console.GetDataFromUser("Podaj datę przyjęcia surowca chemicznego: "));
                            DateOnly expiriationDate = DateOnly.Parse(_console.GetDataFromUser("Podaj datę ważności surowca chemicznego: "));
                            var amount = int.Parse(_console.GetDataFromUser("Podaj ilość surowca chemicznego: "));
                            var storagePlace = _console.GetDataFromUser("Podaj miejsce przechowywania surowca chemicznego: ");
                            var destiny = _console.GetDataFromUser("Podaj przeznaczenie surowca chemicznego: ");

                            string index;
                            var amountOfRawMaterials = _mongoRepository.GetDataList();
                            if (amountOfRawMaterials.Count == 0)
                            {
                                index = "RD" + (amountOfRawMaterials.Count + 300000).ToString();
                            }
                            else
                            {
                                var createIndex = (amountOfRawMaterials.Last()._index).Split("D");
                                index = "RD" + (int.Parse(createIndex[1]) + 1);
                            }

                            var newRawMaterial = new RawMaterial()
                            {
                                _index = index,
                                _fullName = name.ToLower(),
                                _supplier = supplier.ToLower(),
                                _acceptanceDate = acceptanceDate,
                                _expiriationDate = expiriationDate,
                                _amount = amount,
                                _storagePlace = storagePlace.ToLower(),
                                _destiny = destiny.ToLower(),
                            };

                            _mongoRepository.addToDb(newRawMaterial);
                            _console.Clear();

                            _console.WriteLine($"Index surowca chemicznego: {newRawMaterial._index}");
                            _console.WriteLine($"Nazwa surowca chemicznego: {newRawMaterial._fullName}");
                            _console.WriteLine($"Dostawca surowca chemicznego: {newRawMaterial._supplier}");
                            _console.WriteLine($"Data przyjęcia surowca chemicznego: {newRawMaterial._acceptanceDate}");
                            _console.WriteLine($"Data ważności surowca chemicznego: {newRawMaterial._expiriationDate}");
                            _console.WriteLine($"Ilość surowca chemicznego: {newRawMaterial._amount}");
                            _console.WriteLine($"Miejsce składowania surowca chemicznego: {newRawMaterial._storagePlace}");
                            _console.WriteLine($"Przeznaczenie: {newRawMaterial._destiny}");
                            _console.WriteLine("Dodano do bazy danych");
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
                            var rawMaterialsList = _mongoRepository.GetDataList();
                            _console.Clear();

                            foreach (var rawMaterial in rawMaterialsList)
                            {
                                _console.WriteLine($"Index: {rawMaterial._index}");
                                _console.WriteLine($"Nazwa: {rawMaterial._fullName}");
                                _console.WriteLine($"Dostawca: {rawMaterial._supplier}");
                                _console.WriteLine($"Data przyjęcia: {rawMaterial._acceptanceDate}");
                                _console.WriteLine($"Data ważności: {rawMaterial._expiriationDate}");
                                _console.WriteLine($"Ilość surowca chemicznego: {rawMaterial._amount}");
                                _console.WriteLine($"Miejsce składowania: {rawMaterial._storagePlace}");
                                _console.WriteLine($"Przeznaczenie: {rawMaterial._destiny}");
                                _console.WriteLine("");
                            }

                            _console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
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
                            var searchTerm = _console.GetDataFromUser("Podaj indeks edytowanego surowca chemicznego: ");
                            var updatingRawMaterial = _mongoRepository.GetData(searchTerm);

                            if (updatingRawMaterial != null)
                            {
                                var name = _console.GetDataFromUser("Podaj nazwę surowca chemicznego: ");
                                var supplier = _console.GetDataFromUser("Podaj nazwę dostawcy surowca chemicznego: ");
                                DateOnly acceptanceDate = DateOnly.Parse(_console.GetDataFromUser("Podaj datę przyjęcia surowca chemicznego: "));
                                DateOnly expiriationDate = DateOnly.Parse(_console.GetDataFromUser("Podaj datę ważności surowca chemicznego: "));
                                var amount = int.Parse(_console.GetDataFromUser("Podaj ilość surowca chemicznego: "));
                                var storagePlace = _console.GetDataFromUser("Podaj miejsce przechowywania surowca chemicznego: ");
                                var destiny = _console.GetDataFromUser("Podaj przeznaczenie surowca chemicznego: ");

                                updatingRawMaterial._fullName = name.ToLower();
                                updatingRawMaterial._supplier = supplier.ToLower();
                                updatingRawMaterial._acceptanceDate = acceptanceDate;
                                updatingRawMaterial._expiriationDate = expiriationDate;
                                updatingRawMaterial._amount = amount;
                                updatingRawMaterial._storagePlace = storagePlace.ToLower();
                                updatingRawMaterial._destiny = destiny.ToLower();

                                _mongoRepository.UpdateData("_index", searchTerm, updatingRawMaterial);
                                _console.WriteLine("Dane zaktualizowane");
                            }
                            else
                            {
                                _console.WriteLine("Nie istnieje obiekt o takim indeksie!");
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
                            var searchTerm = _console.GetDataFromUser("Podaj indeks usuwanego surowca chemicznego: ");
                            var deletingRawMaterial = _mongoRepository.GetData(searchTerm);

                            if (deletingRawMaterial != null)
                            {
                                _mongoRepository.DeleteData(searchTerm);
                                _console.WriteLine($"Surowiec o indeksie {deletingRawMaterial._index} został usunięty!");

                            }
                            else
                            {
                                _console.WriteLine("Nie istnieje obiekt o takim indeksie!");
                            }

                            _console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            _console.WriteLine(e.Message);
                            _console.ReadLine();
                        }
                        break;

                    case 5:
                        return;
                }
            }
        }
    }
}

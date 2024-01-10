using projectDydaTomasz.Core.Interfaces;
using projectDydaTomasz.Core.Models;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectDydaTomasz.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IDatabaseConnection<Car> _carRepository;

        public CarService(IDatabaseConnection<Car> carService)
        {
            _carRepository = carService;
        }

        public List<Car> GetCars(string searchTerm)
        {
            var carList = _carRepository.GetFilteredDataList("user.userId", searchTerm);
            return carList;
        }

        public List<Car> GetAllCarsList()
        {
            var carList = _carRepository.GetAllDataList();
            return carList;
        }

        public void UpdateCar(Car updatingCar)
        {
            _carRepository.UpdateData("carId", updatingCar.carId, updatingCar);
        }

        public void DeleteCar(string searchTerm)
        {
            _carRepository.DeleteData("carId", searchTerm);
        }
    }
}

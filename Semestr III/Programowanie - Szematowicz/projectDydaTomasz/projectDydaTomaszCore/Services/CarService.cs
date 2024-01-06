using projectDydaTomasz.Core.Interfaces;
using projectDydaTomasz.Core.Models;
using projectDydaTomaszCore.Interfaces;
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

        public List<Car> GetCars(string model)
        {
            var carList = _carRepository.GetFilteredDataList("user.userId", model);
            return carList;
        }

        public List<Car> GetAllCarsList()
        {
            var carList = _carRepository.GetAllDataList();
            return carList;
        }
    }
}

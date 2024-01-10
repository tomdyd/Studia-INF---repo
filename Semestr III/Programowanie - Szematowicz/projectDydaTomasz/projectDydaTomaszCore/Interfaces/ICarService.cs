using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectDydaTomasz.Core.Models;
using projectDydaTomaszCore.Models;

namespace projectDydaTomasz.Core.Interfaces
{
    public interface ICarService
    {
        public List<Car> GetCars(string searchTerm);

        //public List<Car> GetAllCarsList();
        public void UpdateCar(Car updatingCar);
        public void DeleteCar(string searchTerm);
    }
}
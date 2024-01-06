using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectDydaTomasz.Core.Models;

namespace projectDydaTomasz.Core.Interfaces
{
    public interface ICarService
    {
        public List<Car> GetCars(string model);

        public List<Car> GetAllCarsList();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectDydaTomasz.Core.Models
{
    public class Car
    {
        public string carId { get; set; }
        public string carBrand { get; set; }
        public string carModel { get; set; }
        public int carProductionYear{ get; set; }
        public double engineCapacity { get; set; }
    }
}

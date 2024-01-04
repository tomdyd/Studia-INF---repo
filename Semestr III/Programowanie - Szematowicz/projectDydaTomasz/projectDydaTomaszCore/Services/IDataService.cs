using projectDydaTomaszCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectDydaTomaszCore.Services
{
    public interface IDataService<T>
    {
        public T[] GetAllData();
    }
}

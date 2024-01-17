using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToMongoDbRawMaterials.Core.Interfaces
{
    public interface IMongoClient<T>
    {
        void Connect();
        void addToDb(T input);
        List<T> GetDataList();
        T GetData(string searchTerm);
        void UpdateData(string property, string searchTerm, T updatingData);
        void DeleteData(string searchTerm);
    }
}

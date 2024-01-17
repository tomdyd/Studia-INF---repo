using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectToMongoDbRawMaterials.Interfaces
{
    public interface IAppConsole
    {
        void WriteLine(string message);
        void Write(string messsage);
        void Clear();
        string ReadLine();
        int GetResponeFromUser();
        string GetDataFromUser(string message);

    }
}

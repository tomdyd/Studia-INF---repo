using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpolacjaNewtona.Interfaces
{
    public interface IAppConsole
    {
        void WriteLine(string message);
        void Write(string message);
        string ReadLine();
        void Clear();
    }
}

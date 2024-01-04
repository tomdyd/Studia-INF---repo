using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using usbEventDetector.Interfaces;

namespace usbEventDetector
{
    public class AppRunner
    {
        private readonly IConsole _console;

        public AppRunner(IConsole console)
        {
            _console = console;
        }

        public void StartApp()
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 or EventType = 3");
            ManagementEventWatcher watcher = new ManagementEventWatcher(query);
            USBEvent eve = new USBEvent();
            watcher.EventArrived += new EventArrivedEventHandler(eve.EventArrived);
            watcher.Start();

            _console.WriteLine("Press any key to exit");
            Console.ReadKey();

            watcher.Stop();
        }
    }
}

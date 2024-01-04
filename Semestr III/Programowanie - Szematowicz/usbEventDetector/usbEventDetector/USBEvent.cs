using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using usbEventDetector.Interfaces;

namespace usbEventDetector
{
    public class USBEvent : IEvent
    {
        private static DateTime lastEventTime = DateTime.MinValue;
        private static TimeSpan debounceInterval = TimeSpan.FromSeconds(0.1); // Set your debounce interval here (1 second in this example)

        public void EventArrived(object sender, EventArrivedEventArgs e)
        {
            DateTime currentEventTime = DateTime.Now;
            if (currentEventTime - lastEventTime > debounceInterval)
            {
                PropertyData pd = e.NewEvent.Properties["EventType"];
                if (pd != null && pd.Value.ToString() == "2")
                {
                    Console.WriteLine("USB Device Plugged In");
                }
                else if (pd != null && pd.Value.ToString() == "3")
                {
                    Console.WriteLine("USB Device Unplugged");
                }
                lastEventTime = currentEventTime;
            }
        }
    }
}

using System.Management;

namespace usbEventDetector.Interfaces
{
    public interface IEvent
    {
        public void EventArrived(object sender, EventArrivedEventArgs e);
    }
}
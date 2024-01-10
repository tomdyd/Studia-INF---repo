namespace usbEventDetector.Interfaces
{
    public interface IConsole
    {
        public void WriteLine(string msg);

        public void ConsoleClear();

        public string ReadLine();
    }
}
namespace interpolacjaNewtona.Interfaces
{
    public interface IAppConsole
    {
        void WriteLine(string message);
        void Write(string message);
        string ReadLine();
        ConsoleKeyInfo ReadKey();
        void Clear();
        int getIntFromUser(string msg);
        double getDoubleFromUser(string msg);
        void Siganture();
        
    }
}

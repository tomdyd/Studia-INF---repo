namespace projectDydaTomasz.Interfaces
{
    public interface IAppConsole
    {
        int GetResponseFromUser();

        void Clear();

        string ReadLine();

        void WriteLine(object msg);
    }
}
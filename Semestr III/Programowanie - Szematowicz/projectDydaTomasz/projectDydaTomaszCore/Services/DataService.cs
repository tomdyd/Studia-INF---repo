using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;

namespace projectDydaTomaszCore.Services
{
    public class DataService<T> : IDataService<T>
    {
        private readonly IDatabaseConnection<T> _userRepository;

        public DataService(IDatabaseConnection<T> userService)
        {
            _userRepository = userService;
        }

        public T[] GetAllData()
        {
            var users = _userRepository.GetUsers();
            return users;
        }
    }
}

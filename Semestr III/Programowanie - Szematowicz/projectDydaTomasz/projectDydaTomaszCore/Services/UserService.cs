using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;

namespace projectDydaTomaszCore.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseConnection<User> _userRepository;

        public UserService(IDatabaseConnection<User> userService)
        {
            _userRepository = userService;
        }

        public User GetUser(string username)
        {
            var users = _userRepository.GetFilteredData(username);
            return users;
        }
    }
}

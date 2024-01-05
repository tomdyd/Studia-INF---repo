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

        public User GetUser(string username, string password)
        {
            var users = _userRepository.GetData();
            var user = users.Find(x => x.Username == username);
            if (user.PasswordHash == password)
            {
                return user;       
            }
            return null;
        }
    }
}

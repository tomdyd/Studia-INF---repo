using projectDydaTomasz.Core.Interfaces;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using System;
using System.Runtime.CompilerServices;

namespace projectDydaTomaszCore.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseConnection<User> _userRepository;

        public UserService(IDatabaseConnection<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User AuthorizeUser(string username, string password)
        {
            var user = _userRepository.GetFilteredData("username", username);

            if (user != null)
            {
                if (user.username == username && user.passwordHash == password)
                {
                    Console.WriteLine("Zalogowano poprawnie");
                    Console.ReadLine();
                    return user;
                }
                else
                {
                    Console.WriteLine("Niepoprawne dane!");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("Niepoprawne dane!");
            Console.ReadLine();
            return null;
        }
    }
}

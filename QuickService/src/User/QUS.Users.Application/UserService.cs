using QUS.Users.Domain.Interfaces;
using QUS.Users.Domain.Models;

namespace QUS.Users.Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void CreateUser(string name, string email, string phone)
        {
            var user =  new User(name, email, phone);

            _userRepository.Add(user);
        }
    }
}

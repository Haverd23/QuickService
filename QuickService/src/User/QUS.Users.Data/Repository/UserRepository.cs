

using QUS.Users.Domain.Interfaces;
using QUS.Users.Domain.Models;

namespace QUS.Users.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public Task Add(User user)
        {
           _context.Users.Add(user);
           _context.SaveChanges();
           return Task.CompletedTask;
        }
    }
}

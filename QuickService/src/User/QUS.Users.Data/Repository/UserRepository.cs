

using QUS.Core.Data;
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

        public IUnityOfWork UnitOfWork => _context;

        public async Task Add(User user)
        {
          await _context.Users.AddAsync(user);
          
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

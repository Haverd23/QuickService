

using Microsoft.EntityFrameworkCore;
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
        public async Task<User> GetById(Guid id)
        {
           var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
           if(user == null)
           {
            throw new Exception($"User with id {id} not found.");
            }
           return user;
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
      
        }
        

        public void Dispose()
        {
            _context?.Dispose();
        }

        
    }
}

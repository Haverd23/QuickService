using Microsoft.EntityFrameworkCore;
using QUS.Auth.Domain.Interfaces;
using QUS.Auth.Domain.Models;
using QUS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;
        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task AddAsync(User user)
        {
            await _context.UsersAuth.AddAsync(user);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
           return await _context.UsersAuth.Where(e => e.Email.Entrada == email).
                FirstOrDefaultAsync();
        }
        public async Task DeleteAsync(Guid user)
        {
            var userToDelete = 
                await _context.UsersAuth.FirstOrDefaultAsync(u => u.Id == user);
            if (userToDelete != null)
                {
                _context.UsersAuth.Remove(userToDelete);
            }

        }

        public void Dispose()
        {
            _context?.Dispose();
        }

       
    }
}

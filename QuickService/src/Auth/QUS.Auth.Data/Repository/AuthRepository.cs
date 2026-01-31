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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

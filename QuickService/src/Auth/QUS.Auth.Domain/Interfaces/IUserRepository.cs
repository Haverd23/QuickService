using QUS.Auth.Domain.Models;
using QUS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddAsync(User user);
    }
}

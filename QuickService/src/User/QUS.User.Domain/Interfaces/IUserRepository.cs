using QUS.Core.Data;
using QUS.Users.Domain.Models;
namespace QUS.Users.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task Add(User user);
    }
}

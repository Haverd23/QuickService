using QUS.Users.Domain.Models;
namespace QUS.Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
    }
}

using QUS.Core.Data;
using QUS.Services.Domain.Models;


namespace QUS.Services.Domain.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task AddAsync(Service service);
        Task GetUserIdAsync(Guid serviceId);
    }
}

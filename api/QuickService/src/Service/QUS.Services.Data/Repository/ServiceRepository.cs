using Microsoft.EntityFrameworkCore;
using QUS.Core.Data;
using QUS.Services.Domain.Interfaces;
using QUS.Services.Domain.Models;
namespace QUS.Services.Data.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceDbContext _context;
        public ServiceRepository(ServiceDbContext context)
        {
            _context = context;
        }
        public IUnityOfWork UnitOfWork => _context;

        public async Task AddAsync(Service service)
        {
           await _context.Services.AddAsync(service);
        }
        public async Task GetUserIdAsync(Guid serviceId)
        {
           await _context.Services.Where(s => s.Id == serviceId).FirstOrDefaultAsync();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

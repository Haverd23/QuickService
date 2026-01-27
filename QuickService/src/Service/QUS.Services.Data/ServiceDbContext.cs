
using Microsoft.EntityFrameworkCore;
using QUS.Core.Data;
using QUS.Services.Domain.Models;

namespace QUS.Services.Data
{
    public class ServiceDbContext : DbContext, IUnityOfWork
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        {

        }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            try
            {
                var result = await SaveChangesAsync();
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using QUS.Auth.Domain.Models;
using QUS.Core.Data;

namespace QUS.Auth.Data
{
    public class AuthDbContext : DbContext, IUnityOfWork
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        public DbSet<User> UsersAuth { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
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

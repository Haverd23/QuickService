using Microsoft.EntityFrameworkCore;
using QUS.Core.Data;
using QUS.Users.Domain.Models;

namespace QUS.Users.Data
{
    public class UserDbContext : DbContext, IUnityOfWork
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
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

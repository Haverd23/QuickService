using Microsoft.EntityFrameworkCore;
using QUS.Users.Domain.Models;
namespace QUS.Users.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);

                user.OwnsOne(u => u.Phone, phone =>
                {
                    phone.Property(p => p.Number)
                         .HasColumnName("Phone")
                         .IsRequired();
                });
            });
        }

    }
}

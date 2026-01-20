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
    }
}

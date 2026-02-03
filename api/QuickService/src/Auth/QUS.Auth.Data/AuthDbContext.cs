using Microsoft.EntityFrameworkCore;
using QUS.Auth.Domain.Models;
using QUS.Core.Data;
using QUS.Core.DomainObjects;

namespace QUS.Auth.Data
{
    public class AuthDbContext : DbContext, IUnityOfWork
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        public AuthDbContext(DbContextOptions<AuthDbContext> options, IDomainEventDispatcher
            domainEventDispatcher) : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
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
            var domainEvents = ChangeTracker.Entries<Entity>()
                .SelectMany(e => e.Entity.GetDomainEvents())
                .ToList();

            var result = await SaveChangesAsync();

            await _domainEventDispatcher.DispatchEventsAsync(domainEvents);

            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                entry.Entity.ClearDomainEvents();
            }


            return result > 0;
        }

    }
}

using QUS.Core.DomainObjects;
namespace QUS.Auth.Domain.DomainEvents
{
    public class UserCreateEvent : IDomainEvent
    {
        public Guid AuthId { get; }
        public string Email { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public UserCreateEvent(Guid authId, string email)
        {
            AuthId = authId;
            Email = email;
        }
    }
}

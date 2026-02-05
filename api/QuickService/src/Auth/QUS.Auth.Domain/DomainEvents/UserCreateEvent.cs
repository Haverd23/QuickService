using QUS.Core.DomainObjects;
namespace QUS.Auth.Domain.DomainEvents
{
    public class UserCreateEvent : IDomainEvent
    {
        public string Email { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public Guid AggregateId { get; }

        public UserCreateEvent(Guid aggregateId, string email)
        {
            AggregateId = aggregateId;
            Email = email;
        }
    }
}

namespace QUS.Core.DomainObjects
{
    public class Entity
    {
        private List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Guid Id { get; private set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents ?? Enumerable.Empty<IDomainEvent>();
        }

    }
}

namespace QUS.Core.DomainObjects
{
    public class Entity
    {
        public Guid Id { get; private set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

    }
}

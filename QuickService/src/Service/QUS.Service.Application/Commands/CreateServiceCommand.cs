using QUS.Core.Mediator.Commands;

namespace QUS.Service.Application.Commands
{
    public class CreateServiceCommand : ICommand<Guid>
    {
        public string Title { get; }
        public string Description { get; }
        public decimal Price { get; }
        public Guid ProviderId { get; }
        public string Category { get; }
        public CreateServiceCommand(string title, string description, decimal price,
            Guid providerId, string category)
        {
            Title = title;
            Description = description;
            Price = price;
            ProviderId = providerId;
            Category = category;
        }
        
    }
}

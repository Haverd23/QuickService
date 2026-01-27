using QUS.Core.DomainObjects;
using QUS.Services.Domain.Enums;
namespace QUS.Services.Domain.Models
{
    public class Service : Entity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid ProviderId { get; private set; }
        public Category Category { get; private set; }
        public Service(string title, string description, decimal price, Guid providerId, Category category)
        {
            ServiceValidation(title, description, price);
            Title = title;
            Description = description;
            Price = price;
            ProviderId = providerId;
            Category = category;
        }
        private void TitleValidation(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new Exception("O Título não pode ser vazio");
            if (title.Length < 3)
                throw new Exception("O Título deve possuir mais que 3 caracteres");
        }
        public void ChangeTitle(string title)
        {
            TitleValidation(title);
            Title = title;
        }
        public void ChangeCategory(Category category)
        {
            if(!Enum.IsDefined(typeof(Category), category))
                throw new Exception("Categoria inválida");

            if(Category == category)
                throw new Exception("A nova categoria deve ser diferente da atual");
            Category = category;
        }

        private void DescriptionValidation(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new Exception("A Descrição não pode ser vazia");
            if (description.Length < 10)
                throw new Exception("A Descrição deve possuir mais que 10 caracteres");
        }
        public void ChangeDescription(string description)
        {
            DescriptionValidation(description);
            Description = description;
        }
        private void PriceValidation(decimal price)
        {
            if (price < 0)
                throw new Exception("O Preço não pode ser negativo");
        }
        public void ChangePrice(decimal price)
        {
            PriceValidation(price);
            Price = price;
        }
        private void ServiceValidation(string title, string description, decimal price)
        {
            TitleValidation(title);
            DescriptionValidation(description);
            PriceValidation(price);
        }


    }
}

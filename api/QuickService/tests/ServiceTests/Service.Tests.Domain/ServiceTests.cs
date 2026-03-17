using QUS.Core.Exceptions;
using QUS.Services.Domain.Enums;
using QUS.Services.Domain.Models;


namespace Services.Tests.Domain
{
    public class ServiceTests
    {
        [Fact(DisplayName = "Criar Service com Dados Válidos")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            var title = "Troca de Óleo";
            var description = "Serviço de troca de óleo para veículos.";
            var price = 150;
            var providerId = Guid.NewGuid();
            var category = Category.Mecânico;
            var city = "São Paulo";

            // Act
            var service = new Service(title, description, price, providerId, category,city);

            // Assert
            Assert.NotNull(service);
            Assert.Equal(title, service.Title);
            Assert.Equal(description, service.Description);
            Assert.Equal(price, service.Price);
            Assert.Equal(providerId, service.ProviderId);
            Assert.Equal(category, service.Category);



        }
        [Fact(DisplayName = "Criar Service com Título Vazio lança Exceção")]
        public void Construtor_QuandoTituloVazio_DeveLancarExcecao()
        {
            // Arrange
            var title = ""; // Título vazio
            var description = "Serviço de troca de óleo para veículos.";
            var price = 150;
            var providerId = Guid.NewGuid();
            var category = Category.Mecânico;
            var city = "São Paulo";

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => new Service(title,
                description, price, providerId, category,city));
            Assert.Equal("O Título não pode ser vazio", exception.Message);
        }

        [Fact(DisplayName = "Criar Service com Preço Negativo lança Exceção")]
        public void Construtor_QuandoPrecoNegativo_DeveLancarExcecao()
        {
            // Arrange
            var title = "Troca de Óleo";
            var description = "Serviço de troca de óleo para veículos.";
            var price = -50; // Preço negativo
            var providerId = Guid.NewGuid();
            var category = Category.Mecânico;
            var city = "São Paulo";

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => new Service(title,
                description, price, providerId, category, city));
            Assert.Equal("O Preço não pode ser negativo", exception.Message);
        }

        [Fact(DisplayName = "Criar Service com Descrição Vazia lança Exceção")]
        public void Construtor_QuandoDescricaoVazia_DeveLancarExcecao()
        {
            // Arrange
            var title = "Troca de Óleo";
            var description = ""; // Descrição vazia
            var price = 150;
            var providerId = Guid.NewGuid();
            var category = Category.Mecânico;
            var city = "São Paulo";

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => new Service(title,
                description, price, providerId, category, city));
            Assert.Equal("A Descrição não pode ser vazia", exception.Message);
        }

        [Fact(DisplayName = "Mudar Titulo com Valor Válido")]
        public void MudarTitulo_QuandoValorValido_DeveAtualizarTitulo()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");

            var novoTitulo = "Alinhamento e Balanceamento";

            // Act
            service.ChangeTitle(novoTitulo);

            // Assert
            Assert.Equal(novoTitulo, service.Title);

        }
        [Fact(DisplayName = "Mudar Titulo com Valor Vazio lança Exceção")]
        public void MudarTitulo_QuandoValorVazio_DeveLancarExcecao()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");

            var novoTitulo = ""; // Título vazio

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => service.ChangeTitle(novoTitulo));
            Assert.Equal("O Título não pode ser vazio", exception.Message);
        }
        [Fact(DisplayName = "Mudar Preço com Valor Válido")]
        public void MudarPreco_QuandoValorValido_DeveAtualizarPreco()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");
            var novoPreco = 200;

            // Act
            service.ChangePrice(novoPreco);

            // Assert
            Assert.Equal(novoPreco, service.Price);
        }
        [Fact(DisplayName = "Mudar Preço com Valor Negativo lança Exceção")]
        public void MudarPreco_QuandoValorNegativo_DeveLancarExcecao()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");
            var novoPreco = -100; // Preço negativo

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => service.ChangePrice(novoPreco));
            Assert.Equal("O Preço não pode ser negativo", exception.Message);
        }
        [Fact(DisplayName = "Mudar Descrição com Valor Válido")]
        public void MudarDescricao_QuandoValorValido_DeveAtualizarDescricao()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");
            var novaDescricao = "Serviço completo de alinhamento e balanceamento.";

            // Act
            service.ChangeDescription(novaDescricao);

            // Assert
            Assert.Equal(novaDescricao, service.Description);
        }
        [Fact(DisplayName = "Mudar Descrição com Valor Vazio lança Exceção")]
        public void MudarDescricao_QuandoValorVazio_DeveLancarExcecao()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");
            var novaDescricao = ""; // Descrição vazia

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => service.ChangeDescription(novaDescricao));
            Assert.Equal("A Descrição não pode ser vazia", exception.Message);
        }
        [Fact(DisplayName = "Mudar Categoria")]
        public void MudarCategoria_DeveAtualizarCategoria()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");
            var novaCategoria = Category.Eletricista;

            // Act
            service.ChangeCategory(novaCategoria);

            // Assert
            Assert.Equal(novaCategoria, service.Category);
        }
        [Fact(DisplayName = "Mudar para a mesma categoria lança Exceção")]
        public void MudarCategoria_QuandoMesmaCategoria_DeveLancarExcecao()
        {
            // Arrange
            var service = new Service("Troca de Óleo",
                "Serviço de troca de óleo para veículos.",
                150, Guid.NewGuid(), Category.Mecânico, "São Paulo");
            var novaCategoria = Category.Mecânico; // Mesma categoria
            // Act & Assert

            var exception = Assert.Throws<AppException>(() => service.ChangeCategory(novaCategoria));
            Assert.Equal("A nova categoria deve ser diferente da atual", exception.Message);
        }
    }
}
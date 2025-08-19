using FluentAssertions;
using Moq;
using ProvaPub.Application.Interfaces;
using ProvaPub.Domain.Entities;
using ProvaPub.Presentation.Controllers;
using ProvaPub.Application.DTOs;
using Xunit;

namespace ProvaPub.Tests.Units.Controllers
{
    public class Parte2ControllerTests
    {
        [Fact]
        public void ListProducts_DeveRetornarListaDeProdutos_QuandoServicoRetornaSucesso()
        {
            // Arrange
            var paginatedProducts = new PaginatedList<Product>
            {
                Items = new List<Product> { new Product { Id = 1, Name = "Handmade Frozen Chair" } },
                HasNext = false,
                TotalCount = 1
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ListProducts(1)).Returns(paginatedProducts);

            var mockCustomerService = new Mock<ICustomerService>();
            var controller = new Parte2Controller(mockProductService.Object, mockCustomerService.Object);

            // Act
            var result = controller.ListProducts(1);

            // Assert
            result.Should().BeEquivalentTo(paginatedProducts);
        }

        [Fact]
        public void ListCustomers_DeveRetornarListaDeClientes_QuandoServicoRetornaSucesso()
        {
            // Arrange
            var paginatedCustomers = new PaginatedList<Customer>
            {
                Items = new List<Customer> { new Customer { Id = 1, Name = "Cliente 1" } },
                HasNext = false,
                TotalCount = 1
            };

            var mockProductService = new Mock<IProductService>();
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(s => s.ListCustomers(1)).Returns(paginatedCustomers);

            var controller = new Parte2Controller(mockProductService.Object, mockCustomerService.Object);

            // Act
            var result = controller.ListCustomers(1);

            // Assert
            result.Should().BeEquivalentTo(paginatedCustomers);
        }

        [Fact]
        public void ListProducts_DeveLancarExcecao_QuandoServicoFalha()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ListProducts(It.IsAny<int>())).Throws(new Exception("Erro ao listar produtos"));

            var mockCustomerService = new Mock<ICustomerService>();
            var controller = new Parte2Controller(mockProductService.Object, mockCustomerService.Object);

            // Act
            Action act = () => controller.ListProducts(1);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Erro ao listar produtos");
        }

        [Fact]
        public void ListCustomers_DeveLancarExcecao_QuandoServicoFalha()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(s => s.ListCustomers(It.IsAny<int>())).Throws(new Exception("Erro ao listar clientes"));

            var controller = new Parte2Controller(mockProductService.Object, mockCustomerService.Object);

            // Act
            Action act = () => controller.ListCustomers(1);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Erro ao listar clientes");
        }
    }
}
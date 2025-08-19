using FluentAssertions;
using Moq;
using ProvaPub.Application.Interfaces;
using ProvaPub.Presentation.Controllers;
using Xunit;

public class Parte1ControllerTests
{
    [Fact]
    public async Task Index_DeveRetornarNumeroAleatorio_QuandoServicoRetornaSucesso()
    {
        // Arrange
        var mockRandomService = new Mock<IRandomService>();
        mockRandomService.Setup(s => s.GetRandom()).ReturnsAsync(42);
        var controller = new Parte1Controller(mockRandomService.Object);

        // Act
        var result = await controller.Index();

        // Assert
        result.Should().Be(42);
    }

    [Fact]
    public async Task Index_DeveLancarExcecao_QuandoServicoFalha()
    {
        // Arrange
        var mockRandomService = new Mock<IRandomService>();
        mockRandomService.Setup(s => s.GetRandom()).ThrowsAsync(new System.Exception("Erro ao gerar número"));
        var controller = new Parte1Controller(mockRandomService.Object);

        // Act
        var act = async () => await controller.Index();

        // Assert
        await act.Should().ThrowAsync<System.Exception>()
            .WithMessage("Erro ao gerar número");
    }
}
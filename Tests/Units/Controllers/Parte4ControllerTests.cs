using Xunit;
using Moq;
using ProvaPub.Presentation.Controllers;
using ProvaPub.Application.Interfaces;

public class Parte4ControllerTests
{
    [Fact]
    public async Task CanPurchase_DeveRetornarTrue_QuandoClientePodeComprar()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        mockService.Setup(s => s.CanPurchase(1, 50)).ReturnsAsync(true);

        var controller = new Parte4Controller(mockService.Object);

        // Act
        var result = await controller.CanPurchase(1, 50);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CanPurchase_DeveRetornarFalse_QuandoClienteNaoPodeComprar()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        mockService.Setup(s => s.CanPurchase(2, 200)).ReturnsAsync(false);

        var controller = new Parte4Controller(mockService.Object);

        // Act
        var result = await controller.CanPurchase(2, 200);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CanPurchase_DeveLancarArgumentOutOfRangeException_QuandoCustomerIdInvalido()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        mockService.Setup(s => s.CanPurchase(0, 50)).ThrowsAsync(new ArgumentOutOfRangeException());

        var controller = new Parte4Controller(mockService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => controller.CanPurchase(0, 50));
    }

    [Fact]
    public async Task CanPurchase_DeveLancarArgumentOutOfRangeException_QuandoPurchaseValueInvalido()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        mockService.Setup(s => s.CanPurchase(1, 0)).ThrowsAsync(new ArgumentOutOfRangeException());

        var controller = new Parte4Controller(mockService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => controller.CanPurchase(1, 0));
    }

    [Fact]
    public async Task CanPurchase_DeveLancarInvalidOperationException_QuandoClienteNaoExiste()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        mockService.Setup(s => s.CanPurchase(999, 50)).ThrowsAsync(new InvalidOperationException());

        var controller = new Parte4Controller(mockService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.CanPurchase(999, 50));
    }
}
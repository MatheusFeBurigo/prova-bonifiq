using ProvaPub.Application.Interfaces;

public class PixPaymentService : IPaymentService
{
    private readonly ILogger<PixPaymentService> _logger;
    public PixPaymentService(ILogger<PixPaymentService> logger)
    {
        _logger = logger;
    }

    async Task IPaymentService.ProcessPayment(decimal value, int customerId)
    {
        _logger.LogInformation("Processando pagamento PIX: Valor={Value}, Cliente={CustomerId}", value, customerId);

        await Task.Delay(500);

        var result = new PaymentResult
        {
            Success = true,
            Message = $"Pagamento PIX realizado com sucesso para o cliente {customerId}.",
            ProcessedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Resultado do pagamento PIX: {@Result}", result);

        return;
    }
}
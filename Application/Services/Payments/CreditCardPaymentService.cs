using ProvaPub.Application.Interfaces;

public class CreditCardPaymentService : IPaymentService
{

    private readonly ILogger<CreditCardPaymentService> _logger;

    public CreditCardPaymentService(ILogger<CreditCardPaymentService> logger)
    {
        _logger = logger;
    }

    public async Task ProcessPayment(decimal value, int customerId)
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
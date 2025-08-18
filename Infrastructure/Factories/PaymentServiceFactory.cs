using ProvaPub.Application.Interfaces;

namespace ProvaPub.Infrastructure.Factories
{
    public class PaymentServiceFactory
    {
        private readonly ILogger<PaymentServiceFactory> _logger;
        private readonly IServiceProvider _serviceProvider;

        public PaymentServiceFactory(ILogger<PaymentServiceFactory> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IPaymentService GetPaymentService(string paymentMethod)
        {
            switch (paymentMethod.ToLower())
            {
                case "pix":
                    return _serviceProvider.GetRequiredService<PixPaymentService>();
                case "creditcard":
                    return _serviceProvider.GetRequiredService<CreditCardPaymentService>();
                case "paypal":
                    return _serviceProvider.GetRequiredService<PaypalPaymentService>();
                default:
                    _logger.LogError("Método de pagamento não suportado: {PaymentMethod}", paymentMethod);
                    throw new NotSupportedException("Método de pagamento não suportado");
            }
        }
    }
}
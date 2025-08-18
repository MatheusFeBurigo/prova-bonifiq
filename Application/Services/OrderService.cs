using ProvaPub.Application.Interfaces;
using ProvaPub.Domain.Entities;
using ProvaPub.Infrastructure.Data.Context;
using ProvaPub.Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

namespace ProvaPub.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly TestDbContext _ctx;
        private readonly PaymentServiceFactory _paymentServiceFactory;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            TestDbContext ctx,
            PaymentServiceFactory paymentServiceFactory,
            ILogger<OrderService> logger)
        {
            _ctx = ctx;
            _paymentServiceFactory = paymentServiceFactory;
            _logger = logger;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            _logger.LogInformation("Iniciando pagamento. Método: {PaymentMethod}, Valor: {PaymentValue}, Cliente: {CustomerId}",
                paymentMethod, paymentValue, customerId);

            var paymentService = _paymentServiceFactory.GetPaymentService(paymentMethod);
            await paymentService.ProcessPayment(paymentValue, customerId);

            var order = new Order
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow
            };

            await InsertOrder(order);

            _logger.LogInformation("Pedido inserido com sucesso. Id do cliente: {CustomerId}, Valor: {PaymentValue}, Data UTC: {OrderDateUtc}",
                customerId, paymentValue, order.OrderDate);

            order.OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate,
                TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            return order;
        }

        public async Task<Order> InsertOrder(Order order)
        {
            return (await _ctx.Orders.AddAsync(order)).Entity;
        }

   }
}

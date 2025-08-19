using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProvaPub.Application.DTOs;
using ProvaPub.Application.Interfaces;
using ProvaPub.Domain.Entities;
using ProvaPub.Infrastructure.Data.Context;

namespace ProvaPub.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly TestDbContext _ctx;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(TestDbContext ctx, ILogger<CustomerService> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public PaginatedList<Customer> ListCustomers(int page)
        {
            return PaginationHelper.Paginate(_ctx.Customers.OrderBy(c => c.Id), page, 10);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0)
            {
                _logger.LogWarning("customerId inválido: {CustomerId}", customerId);
                throw new ArgumentOutOfRangeException(nameof(customerId), "O ID do cliente deve ser maior que zero.");
            }

            if (purchaseValue <= 0)
            {
                _logger.LogWarning("purchaseValue inválido: {PurchaseValue}", purchaseValue);
                throw new ArgumentOutOfRangeException(nameof(purchaseValue), "O valor da compra deve ser maior que zero.");
            }

            var customer = await _ctx.Customers.FindAsync(customerId);
            if (customer == null)
            {
                _logger.LogWarning("Cliente não encontrado: {CustomerId}", customerId);
                throw new InvalidOperationException($"Customer Id {customerId} does not exists");
            }

            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
            {
                _logger.LogInformation("Cliente {CustomerId} já realizou compras neste mês.", customerId);
                return false;
            }

            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
            {
                _logger.LogInformation("Cliente {CustomerId} não comprou antes e o valor da compra é maior que 100.", customerId);
                return false;
            }

            if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
            {
                _logger.LogInformation("Tentativa de compra fora do horário permitido para o cliente {CustomerId}.", customerId);
                return false;
            }

            _logger.LogInformation("Cliente {CustomerId} pode realizar a compra.", customerId);
            return true;
        }
    }
}

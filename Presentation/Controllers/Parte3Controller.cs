using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Application.Services;
using ProvaPub.Domain.Entities;
using ProvaPub.Infrastructure.Data.Context;
using ProvaPub.Infrastructure.Factories;

namespace ProvaPub.Presentation.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly PaymentServiceFactory _paymentServiceFactory;
        private readonly ILogger<OrderService> _logger;

        public Parte3Controller(PaymentServiceFactory paymentServiceFactory, ILogger<OrderService> logger)
        {
            _paymentServiceFactory = paymentServiceFactory;
            _logger = logger;
        }

        [HttpGet("orders")]
        public async Task<Order> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
                .Options;

            using var context = new TestDbContext(contextOptions);

            var orderService = new OrderService(context, _paymentServiceFactory, _logger);

            return await orderService.PayOrder(paymentMethod, paymentValue, customerId);
        }
    }
}

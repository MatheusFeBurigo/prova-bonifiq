using ProvaPub.Domain.Entities;

namespace ProvaPub.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId);
        Task<Order> InsertOrder(Domain.Entities.Order order);
    }
}

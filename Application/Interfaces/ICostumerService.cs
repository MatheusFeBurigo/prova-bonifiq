using ProvaPub.Application.DTOs;
using ProvaPub.Domain.Entities;


namespace ProvaPub.Application.Interfaces
{
    public interface ICustomerService
    {
        PaginatedList<Customer> ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}

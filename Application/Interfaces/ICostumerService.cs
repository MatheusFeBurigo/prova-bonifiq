using ProvaPub.Application.DTOs;

namespace ProvaPub.Application.Interfaces
{
    public interface ICustomerService
    {
        CustomerList ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}

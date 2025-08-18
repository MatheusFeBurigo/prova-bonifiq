    namespace ProvaPub.Application.Interfaces
{
    public interface IPaymentService
    {
        Task ProcessPayment(decimal value, int customerId);
    }
}
using ProvaPub.Application.DTOs;
using ProvaPub.Domain.Entities;

namespace ProvaPub.Application.Interfaces
{
    public interface IProductService
    {
        PaginatedList<Product> ListProducts(int page);
    }
}

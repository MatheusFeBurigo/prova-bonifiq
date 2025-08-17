using ProvaPub.Application.DTOs;

namespace ProvaPub.Application.Interfaces
{
    public interface IProductService
    {
        ProductList ListProducts(int page);
    }
}

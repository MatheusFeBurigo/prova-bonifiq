using ProvaPub.Application.DTOs;
using ProvaPub.Application.Interfaces;
using ProvaPub.Domain.Entities;
using ProvaPub.Infrastructure.Data.Context;
namespace ProvaPub.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly TestDbContext _ctx;

        public ProductService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public PaginatedList<Product> ListProducts(int page)
        {
            return PaginationHelper.Paginate(_ctx.Products.OrderBy(p => p.Id), page, 10);
        }
    }
}

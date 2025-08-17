using ProvaPub.Application.DTOs;
using ProvaPub.Application.Interfaces;
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

		public ProductList  ListProducts(int page)
		{
			return new ProductList() {  HasNext=false, TotalCount =10, Products = _ctx.Products.ToList() };
		}

	}
}

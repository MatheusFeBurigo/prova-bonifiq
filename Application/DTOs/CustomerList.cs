using ProvaPub.Domain.Entities;

namespace ProvaPub.Application.DTOs
{
	public class CustomerList
	{
		public List<Customer> Customers { get; set; }
		public int TotalCount { get; set; }
		public bool HasNext { get; set; }
	}
}

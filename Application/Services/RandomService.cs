using Microsoft.EntityFrameworkCore;
using ProvaPub.Application.DTOs;
using ProvaPub.Application.Interfaces;
using ProvaPub.Infrastructure.Data.Context;

namespace ProvaPub.Application.Services
{
	public class RandomService : IRandomService
	{
		private int seed;
        private readonly TestDbContext _ctx;
        
		public RandomService(TestDbContext ctx)
        {
            _ctx = ctx;
            seed = Guid.NewGuid().GetHashCode();
        }

        public async Task<int> GetRandom()
        {
            var rng = new Random();
            int number;

            do
            {
                number = rng.Next(100);
            }
            while (await _ctx.Numbers.AnyAsync(x => x.Number == number));

            _ctx.Numbers.Add(new RandomNumber { Number = number });
            await _ctx.SaveChangesAsync();

            return number;
        }
    }
}

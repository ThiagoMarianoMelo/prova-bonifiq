using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class RandomService : IRandomService
	{
		private readonly int seed;
        private readonly TestDbContext _context;
		public RandomService(TestDbContext dbContext)
        {

            seed = Guid.NewGuid().GetHashCode();

            _context = dbContext;
        }
        public async Task<int> GetRandom()
		{
            var number = new Random(seed).Next(100);
            _context.Numbers.Add(new RandomNumber() { Number = number });
            await _context.SaveChangesAsync();
            return number;
		}

	}
}

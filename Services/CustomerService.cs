using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class CustomerService : BasePaginateDataService<Customer>, ICustomerService
    {
        private readonly TestDbContext _context;

        public CustomerService(TestDbContext dbContext)
        {
            _context = dbContext;
        }

        public PaginationModel<Customer> ListCustomers(int page)
        {
            if (page <= 0)
                throw new ArgumentException("A pagina escolhida não pode ser menor ou igual a 0");

            //Considerando que a primeira pagina seja a 1
            //Busca 11 itens para evitar realizar count no banco causando possível sobrecarga
            var customers = _context.Customers.Skip((page - 1) * 10).Take(11).ToList();

            return CreatePaginatedResponse(customers);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue, DateTime dateTime)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = dateTime.AddMonths(-1);
            var ordersInThisMonth = await _context.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _context.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            //Business Rule: A customer can purchases only during business hours and working days
            if (dateTime.Hour < 8 || dateTime.Hour > 18 || dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                return false;


            return true;
        }

    }
}

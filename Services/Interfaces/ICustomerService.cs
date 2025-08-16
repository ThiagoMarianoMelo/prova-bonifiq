using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService
    {
        public PaginationModel<Customer> ListCustomers(int page);

        public Task<bool> CanPurchase(int customerId, decimal purchaseValue, DateTime dateTime);
    }
}

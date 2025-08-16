using ProvaPub.Models;
using ProvaPub.Models.Response;

namespace ProvaPub.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderCreatedResponse> PayOrder(IPaymentService paymentService, decimal paymentValue, int customerId);

        public Task<Order> InsertOrder(Order order);
    }
}

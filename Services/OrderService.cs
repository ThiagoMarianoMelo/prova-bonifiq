using ProvaPub.Models;
using ProvaPub.Models.Response;
using ProvaPub.Repository;
using ProvaPub.Services.PaymentMethods;

namespace ProvaPub.Services
{
	public class OrderService
	{
        private readonly TestDbContext _context;

        public OrderService(TestDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<OrderCreatedResponse> PayOrder(IPaymentService paymentService, decimal paymentValue, int customerId)
		{
            await paymentService.CreatePayment();

            var entitySaved = await InsertOrder(new Order() //Retorna o pedido para o controller
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow
            });

            var orderResponse = new OrderCreatedResponse(entitySaved);

            return orderResponse;

        }

		public async Task<Order> InsertOrder(Order order)
        {
			var entitySaved = (await _context.Orders.AddAsync(order)).Entity;

            await _context.SaveChangesAsync();

            return entitySaved;
        }
	}
}

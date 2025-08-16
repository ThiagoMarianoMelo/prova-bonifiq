
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.PaymentMethods
{
    public class PixPaymentService : IPaymentService
    {
        public Task CreatePayment()
        {
            //Pagamento com pix

            return Task.CompletedTask;
        }
    }
}

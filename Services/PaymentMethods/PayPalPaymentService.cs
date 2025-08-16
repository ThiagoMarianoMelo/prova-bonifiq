
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.PaymentMethods
{
    public class PayPalPaymentService : IPaymentService
    {
        public Task CreatePayment()
        {
            //Pagamento com PayPal

            return Task.CompletedTask;
        }
    }
}

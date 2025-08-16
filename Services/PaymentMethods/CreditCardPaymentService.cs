
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.PaymentMethods
{
    public class CreditCardPaymentService : IPaymentService
    {
        public Task CreatePayment()
        {
            //Pagamento com cartão de crédito

            return Task.CompletedTask;
        }
    }
}

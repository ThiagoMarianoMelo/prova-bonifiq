namespace ProvaPub.Services.PaymentMethods
{
    public static class PaymentMethodFactory
    {
        public static IPaymentService CreatePaymentService(string paymentMethod)
        {
            return paymentMethod switch
            {
                "pix" => new PixPaymentService(),
                "creditcard" => new CreditCardPaymentService(),
                "paypal" => new PayPalPaymentService(),
                _ => throw new ArgumentException("Tipo de pagamento não identificado ou implementado"),
            };
        }
    }
}

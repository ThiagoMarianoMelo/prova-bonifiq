namespace ProvaPub.Models.Response
{
    public class OrderCreatedResponse
    {
        public OrderCreatedResponse(Order order)
        {
            Id = order.Id;
            Value = order.Value;
            CustomerId = order.CustomerId;
            OrderDate = order.OrderDate.ToLocalTime();
        }

        public int Id { get; set; }
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

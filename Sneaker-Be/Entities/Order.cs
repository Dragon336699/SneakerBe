namespace Sneaker_Be.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? Active { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? Note { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public float TotalMoney { get; set; }
        public string ShippingMethod { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string PaymentMethod { get; set; }
        public List<OrderDetail>? ordersDetail { get; set; } = new List<OrderDetail>();
    }
}

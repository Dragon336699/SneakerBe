namespace Sneaker_Be.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public int? Active { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string phone_number { get; set; }
        public string Address { get; set; }
        public string? Note { get; set; }
        public DateTime order_date { get; set; }
        public string? Status { get; set; }
        public float total_money { get; set; }
        public string shipping_method { get; set; }
        public DateTime? shipping_date { get; set; }
        public string payment_method { get; set; }
        public List<OrderDetail> orders_details { get; set; } = new List<OrderDetail>();
    }
}

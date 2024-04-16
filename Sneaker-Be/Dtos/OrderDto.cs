namespace Sneaker_Be.Dtos
{
    public class OrderDto
    {
        public string fullname { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public string shipping_method { get; set; }
        public string payment_method { get; set; }
        public float total_money { get; set; }
        public IEnumerable<AddProductToCartDto>  cart_items { get; set; }
    }
}

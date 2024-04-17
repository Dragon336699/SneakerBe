using Sneaker_Be.Entities;

namespace Sneaker_Be.Dtos
{
    public class InforOrderDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string phone_number { get; set; }
        public string Address {  get; set; }
        public string Note {  get; set; }
        public string shipping_method {  get; set; }
        public string payment_method { get; set; }
        public DateTime order_date { get; set; }
        public string status { get; set; }
        public List<OrderDetail> order_details { get; set; }
    }
}

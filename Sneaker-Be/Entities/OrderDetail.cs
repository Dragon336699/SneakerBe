namespace Sneaker_Be.Entities
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int? order_id { get; set; }
        public int product_id { get; set; }
        public float price { get; set; }
        public int number_of_products { get; set; }
        public float total_money { get; set; }
        public int size { get; set; }
        public Product? Product { get; set; }
    }
}

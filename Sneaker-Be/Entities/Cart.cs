namespace Sneaker_Be.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public int size { get; set; }
        public List<Product> products { get; set; } = new List<Product> ();
    }
}

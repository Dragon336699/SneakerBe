namespace Sneaker_Be.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public DateTime create_at { get; set; }
        public DateTime updated_at { get; set; }
        public int category_id { get; set; }
        public int sale {  get; set; }
        public List<ProductImage> productImages { get; set; } = new List<ProductImage>();
    }
}

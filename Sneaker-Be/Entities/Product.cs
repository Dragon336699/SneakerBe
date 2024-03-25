namespace Sneaker_Be.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int CategoryId { get; set; }
    }
}

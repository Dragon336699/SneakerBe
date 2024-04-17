using Sneaker_Be.Entities;

namespace Sneaker_Be.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int NumberOfProducts { get; set; }
        public int price { get; set; }
        public int size { get; set; }
        public float totalMoney { get; set; }
        public Product Products { get; set; }
    }
}

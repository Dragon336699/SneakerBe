using Sneaker_Be.Entities;

namespace Sneaker_Be.Dtos
{
    public class ProductInCartDto
    {
        public int id { get; set; }
        public Product products { get; set; }
        public int quantity { get; set; }
        public int size { get; set; }
    }
}

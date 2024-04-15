using Sneaker_Be.Entities;

namespace Sneaker_Be.Dtos
{
    public class AllProductDto
    {
        public IEnumerable<Product> products { get; set; }
        public int totalProducts { get; set; }
    }
}

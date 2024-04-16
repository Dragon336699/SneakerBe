namespace Sneaker_Be.Dtos
{
    public class ProductFromCartDto
    {
        public IEnumerable<ProductInCartDto>  carts { get; set; }
        public int totalCartItems {  get; set; }
    }
}

using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command
{
    public class AddProductToCartCommand : IRequest<string>
    {
        public AddProductToCartDto ProductToCart {  get; set; }
        public int UserId { get; set; }
        public AddProductToCartCommand(AddProductToCartDto productToCart, int userId)
        {
            ProductToCart = productToCart;
            UserId = userId;
        }

    }
}

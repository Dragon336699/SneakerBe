using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command.CartCommand
{
    public class UpdateCartCommand : IRequest<string>
    {
        public int Id { get; set; }
        public AddProductToCartDto Product { get; set; }
        public UpdateCartCommand(int id, AddProductToCartDto product)
        {
            Id = id;
            Product = product;
        }
    }
}

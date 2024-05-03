using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command.ProductCommand
{
    public class UpdateProductCommand : IRequest<Boolean>
    {
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
        public UpdateProductCommand(ProductDto product, int productId)
        {
            Product = product;
            ProductId = productId;
        }
    }
}

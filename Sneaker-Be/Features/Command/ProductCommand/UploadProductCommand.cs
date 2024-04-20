using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command.ProductCommand
{
    public class UploadProductCommand : IRequest<int>
    {
        public ProductDto Product { get; set; }
        public UploadProductCommand(ProductDto product)
        {
            Product = product;
        }
    }
}

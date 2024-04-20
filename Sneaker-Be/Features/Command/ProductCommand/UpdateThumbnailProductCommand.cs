using MediatR;

namespace Sneaker_Be.Features.Command.ProductCommand
{
    public class UpdateThumbnailProductCommand : IRequest<Boolean>
    {
        public int ProductId { get; set; }
        public UpdateThumbnailProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}

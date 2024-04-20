using MediatR;

namespace Sneaker_Be.Features.Command.ProductCommand
{
    public class UploadProductImageCommand : IRequest<Boolean>
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public UploadProductImageCommand(int id, string imageUrl)
        {
            Id = id;
            ImageUrl = imageUrl;
        }
    }
}

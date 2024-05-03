using MediatR;

namespace Sneaker_Be.Features.Command.ProductCommand
{
    public class DeleteProductCommand : IRequest<Boolean>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}

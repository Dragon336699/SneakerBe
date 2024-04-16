using MediatR;

namespace Sneaker_Be.Features.Command.CartCommand
{
    public class DeleteProductFromCartCommand : IRequest<string>
    {
        public int Id { get; set; }
        public DeleteProductFromCartCommand(int id)
        {
            Id = id;
        }
    }
}

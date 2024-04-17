using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Command.OrderCommand
{
    public class PostOrderCommand : IRequest<int>
    {
        public Order Order {  get; set; }
        public PostOrderCommand(Order order)
        {
            Order = order;
        }
    }
}

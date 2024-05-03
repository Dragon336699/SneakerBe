using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command.OrderCommand
{
    public class ChangeOrderStateCommand : IRequest<Boolean>
    {
        public string State { get; set; }
        public int OrderId { get; set; }
        public ChangeOrderStateCommand(string state, int orderId)
        {
            State = state;
            OrderId = orderId;
        }
    }
}

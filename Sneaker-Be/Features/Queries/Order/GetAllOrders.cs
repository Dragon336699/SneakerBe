using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.Order
{
    public class GetAllOrders : IRequest<IEnumerable<HistoryOrderDto>>
    {
    }
}

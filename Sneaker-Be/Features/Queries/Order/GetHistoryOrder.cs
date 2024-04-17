using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.Order
{
    public class GetHistoryOrder : IRequest<List<HistoryOrderDto>>
    {
        public int UserId { get; set; }
        public GetHistoryOrder(int userId)
        {
            UserId = userId;
        }
    }
}

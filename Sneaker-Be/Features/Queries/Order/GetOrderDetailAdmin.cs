using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.Order
{
    public class GetOrderDetailAdmin : IRequest<InforOrderDto>
    {
        public int Id { get; set; }
        public GetOrderDetailAdmin(int id)
        {
            Id = id;
        }
    }
}

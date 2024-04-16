using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries
{
    public class GetCart : IRequest<ProductFromCartDto>
    {
        public int UserId { get; set; }
        public GetCart(int userId)
        {
            UserId = userId;
        }
    }
}

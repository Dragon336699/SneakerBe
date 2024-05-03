using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.UserQuery
{
    public class GetAllUser : IRequest<IEnumerable<UserDetailDto>>
    {
    }
}

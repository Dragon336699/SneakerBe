using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Queries.UserQuery
{
    public class GetUserByPhone : IRequest<User>
    {
        public string PhoneNumber { get; set; }
        public GetUserByPhone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}

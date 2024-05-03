using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command.UserCommand
{
    public class DeleteUserCommand : IRequest<IEnumerable<UserDetailDto>>
    {
        public int UserId { get; set; }
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}

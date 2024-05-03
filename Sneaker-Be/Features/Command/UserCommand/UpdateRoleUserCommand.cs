using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Command.UserCommand
{
    public class UpdateRoleUserCommand : IRequest<IEnumerable<UserDetailDto>>
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public UpdateRoleUserCommand(int roleId, int userId)
        {
            RoleId = roleId;
            UserId = userId;
        }
    }
}

using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Command.UserCommand;

namespace Sneaker_Be.Handler.CommandHandler.UserCommand
{
    public class UpdateRoleUserCommandHandler : IRequestHandler<UpdateRoleUserCommand, IEnumerable<UserDetailDto>>
    {
        private readonly DapperContext _dapperContext;
        public UpdateRoleUserCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<UserDetailDto>> Handle(UpdateRoleUserCommand request, CancellationToken cancellationToken)
        {
            var query = "UPDATE users SET role_id=@RoleId WHERE id=@Id " +
                "SELECT * FROM users";
            var param = new DynamicParameters();
            param.Add("RoleId", request.RoleId);
            param.Add("Id", request.UserId);

            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var users = await connection.QueryAsync<UserDetailDto>(query, param);
                    return users.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
                
            }

        }
    }
}

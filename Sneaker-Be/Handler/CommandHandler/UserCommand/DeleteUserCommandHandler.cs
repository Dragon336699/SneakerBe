using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Command.UserCommand;

namespace Sneaker_Be.Handler.CommandHandler.UserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IEnumerable<UserDetailDto>>
    {
        private readonly DapperContext _dapperContext;
        public DeleteUserCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<UserDetailDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var query = "DELETE FROM users WHERE id=@Id " +
                "SELECT * FROM users";

            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var users = await connection.QueryAsync<UserDetailDto>(query, new {Id = request.UserId});
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

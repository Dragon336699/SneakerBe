using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Queries.UserQuery;

namespace Sneaker_Be.Handler.QueryHandler.UserQuery
{
    public class GetAllUserHandler : IRequestHandler<GetAllUser, IEnumerable<UserDetailDto>>
    {
        private readonly DapperContext _dapperContext;
        public GetAllUserHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<UserDetailDto>> Handle(GetAllUser request, CancellationToken cancellationToken)
        {
            var query = "SELECT id, fullname, address, phone_number, role_id, date_of_birth FROM users";
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var users = await connection.QueryAsync<UserDetailDto>(query);
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

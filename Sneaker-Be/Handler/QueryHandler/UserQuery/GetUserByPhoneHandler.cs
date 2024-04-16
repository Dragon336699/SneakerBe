using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.UserQuery;

namespace Sneaker_Be.Handler.QueryHandler.UserQuery
{
    public class GetUserByPhoneHandler : IRequestHandler<GetUserByPhone, User>
    {
        private readonly DapperContext _dapperContext;
        public GetUserByPhoneHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<User> Handle(GetUserByPhone request, CancellationToken cancellationToken)
        {
            var query = "SELECT u.id,u.fullname,u.address,u.phone_number,u.date_of_birth FROM users u WHERE phone_number=@PhoneNumber";
            using (var connection = _dapperContext.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { request.PhoneNumber });
                if (user == null) { return null; }
                return user;
            }
        }
    }
}

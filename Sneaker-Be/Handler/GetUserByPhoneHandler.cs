﻿using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;

namespace Sneaker_Be.Handler
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
            var query = "SELECT * FROM users WHERE phone_number=@PhoneNumber";
            using (var connection = _dapperContext.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query,new { request.PhoneNumber });
                if (user == null) { return null; }
                return user;
            }
        }
    }
}
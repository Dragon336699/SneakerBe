using Dapper;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.UserCommand;
using System.Security.Cryptography;

namespace Sneaker_Be.Handler.CommandHandler.UserCommand
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly DapperContext _dapperContext;
        public RegisterUserCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var query = "INSERT INTO users (fullname, phone_number, address, password, created_at, updated_at, is_active, date_of_birth, facebook_account_id,google_account_id,role_id) " +
            "SELECT @FullName, @PhoneNumber, @Address, @Password,@CreatedAt, @UpdatedAt, @IsActive,@DateOfBirth, @Facebook,@Google,@RoleId " +
            "WHERE NOT EXISTS (SELECT * FROM users WHERE phone_number = @PhoneNumber);";
            var queryUser = "SELECT * FROM users WHERE phone_number = @PhoneNumber;";
            byte[] salt = new byte[]
            {
                153, 9, 194, 39, 4, 123, 47, 99, 167, 242, 240, 77, 130, 225, 71, 96
            };

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: request.Password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            var parameters = new DynamicParameters();
            parameters.Add("FullName", request.FullName);
            parameters.Add("PhoneNumber", request.phone_number);
            parameters.Add("Address", request.Address);
            parameters.Add("Password", hashedPassword);
            parameters.Add("CreatedAt", DateTime.Now);
            parameters.Add("UpdatedAt", DateTime.Now);
            parameters.Add("IsActive", 1);
            parameters.Add("DateOfBirth", request.date_of_birth);
            parameters.Add("Facebook", 0);
            parameters.Add("Google", 0);
            parameters.Add("RoleId", 1);
            parameters.Add("DateOfBirth", request.date_of_birth);

            using (var connection = _dapperContext.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, parameters);
                var user = await connection.QueryAsync(queryUser, parameters);
                if (rowAffected > 0)
                {
                    return "Đăng ký thành công";
                }
                else
                {
                    if (user != null)
                    {
                        return "Số điện thoại đã được sử dụng";
                    }
                    return "Đăng ký không thành công";
                }
            }
        }
    }
}

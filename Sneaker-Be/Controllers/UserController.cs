using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command.UserCommand;
using Sneaker_Be.Features.Queries.UserQuery;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Twilio;
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        public UserController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("users/register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            var res = await _mediator.Send(new RegisterUserCommand(user.FullName, user.Address,user.Password,user.phone_number,user.date_of_birth));
            if (res == "Đăng ký thành công")
            {
                return Ok(new {message = res});
            } else
            {
                return BadRequest(new { message = res });
            }
        }

        [HttpPost]
        [Route("users/login")]
        public async Task<IActionResult> Login(LoginDto loginDetail)
        {
            var user = await _mediator.Send(new GetUserByPhone(loginDetail.PhoneNumber));
            
            if (user == null) { return BadRequest(new
            {
                message = "Tài khoản chưa được đăng ký"
            }); 
            }
            byte[] salt = new byte[]
            {
                153, 9, 194, 39, 4, 123, 47, 99, 167, 242, 240, 77, 130, 225, 71, 96
            };
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: loginDetail.Password!,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA256,
               iterationCount: 100000,
               numBytesRequested: 256 / 8));
            if (user.Password.Equals(hashedPassword))
            {
                string token = GenerateJSonWebToken(user);
                return Ok(new
                {
                    message = "Đăng nhập thành công",
                    token = token
                });
            }
            return BadRequest(new
            {
                message = "Mật khẩu không chính xác"
            });
        }

        [HttpGet]
        [Route("users/details")]
        [Authorize]
        public async Task<IActionResult> GetProfileUser()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var phoneNumber = jwt.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value;
            var user = await _mediator.Send(new GetUserByPhone(phoneNumber));
            var userDetail = new UserDetailDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Address = user.Address,
                phone_number = user.phone_number,
                date_of_birth = user.date_of_birth,
                role_id = user.role_id,
            };
            if (user == null) { return BadRequest("Lỗi thông tin"); }
            return Ok(userDetail);
        }

        [HttpGet]
        [Route("users/getAll")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetAllUser()
        {
            var res = await _mediator.Send(new GetAllUser());
            if (res != null)
            {
                return Ok(res);
            } return BadRequest();
        }

        [HttpPut]
        [Route("users/changeRole/{userId}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> UpdateRoleUser([FromBody] int roleId, int userId)
        {
            var res = await _mediator.Send(new UpdateRoleUserCommand(roleId, userId));
            if (res != null)
            {
                return Ok(new
                {
                    users = res,
                    message = "Đổi vai trò người dùng thành công"
                });
            }
            return BadRequest(new
            {
                message = "Đổi vai trò người dùng thất bại"
            });

        }

        [HttpDelete]
        [Route("users/delete/{userId}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var res = await _mediator.Send(new DeleteUserCommand(userId));
            if (res != null)
            {
                return Ok(new
                {
                    users = res,
                    message = "Xóa người dùng thành công"
                });
            }
            return BadRequest(new
            {
                message = "Xóa người dùng thất bại"
            });

        }

        private string GenerateJSonWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("PhoneNumber", user.phone_number),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.role_id.ToString()),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                 _configuration["Jwt:Issuer"],
                 claims,
                 expires: DateTime.Now.AddDays(30),
                 signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

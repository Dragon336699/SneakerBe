using MediatR;
using Microsoft.AspNetCore.Authorization;
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
using System.Text;
using Twilio;
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
            if (user == null) { return BadRequest("Người dùng không tồn tại"); }
            string token = GenerateJSonWebToken(user);
            return  Ok(new {
                message = "Đăng nhập thành công",
                token = token 
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
            var claims = jwt.Claims.FirstOrDefault(c => c.Type == "PhoneNumber");
            var phoneNumber = claims.Value;
            var user = await _mediator.Send(new GetUserByPhone(phoneNumber));
            if (user == null) { return BadRequest("Lỗi thông tin"); }
            return Ok(user);
        }

        private string GenerateJSonWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("PhoneNumber", user.phone_number),
                new Claim("UserId", user.Id.ToString()),
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

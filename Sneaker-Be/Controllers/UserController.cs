using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command;
using Sneaker_Be.Features.Queries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        private string _myToken ;
        public UserController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            var res = await _mediator.Send(new RegisterUserCommand(user.FullName, user.Address,user.Password,user.phone_number,user.date_of_birth));
            if (res == "Đăng ký thành công")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDetail)
        {
            //IActionResult response = Unauthorized();
            var user = await _mediator.Send(new GetUserByPhone(loginDetail.PhoneNumber));
            if (user == null) { return BadRequest("Người dùng không tồn tại"); }
            string token = GenerateJSonWebToken(user);
            _myToken = token;
            return  Ok(new { token = token });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfileUser()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(_myToken);
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

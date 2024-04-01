using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class SneakerController : Controller
    {
        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        public SneakerController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("categories")]
        [Authorize]
        public async Task<IActionResult> GetCategories()
        {
            string userPhoneNumber = User.Claims.First().Value;
            return Ok(await _mediator.Send(new GetCategories()));
        }

        [HttpPost]
        public async Task<IActionResult> GetUser(string phoneNumber)
        {
            var user = await _mediator.Send(new GetUserByPhone(phoneNumber));
            if (user == null) { return BadRequest("Người dùng không tồn tại"); }
            string token = GenerateJSonWebToken(user);
            return Ok(token);
        }

        private string GenerateJSonWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                new Claim("PhoneNumber", user.phone_number),
                new Claim("DateOfBirth", user.date_of_birth.ToString("yyyy-MM-dd")),
                new Claim("UserId", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"],claims, expires: DateTime.Now.AddDays(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

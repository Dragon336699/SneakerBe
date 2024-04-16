using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Command;
using Sneaker_Be.Features.Queries;
using System.IdentityModel.Tokens.Jwt;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        public CartController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("carts")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartDto product)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var claims = jwt.Claims.FirstOrDefault(c => c.Type == "UserId");
            var usersId = claims.Value;
            var res = await  _mediator.Send(new AddProductToCartCommand(product, Int32.Parse(usersId)));
            if (res == "Thêm sản phẩm vào giỏ hàng thành công")
            {
                return Ok(res);
            } return BadRequest(res);
        }

    }
}

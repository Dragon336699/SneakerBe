using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Command.CartCommand;
using Sneaker_Be.Features.Queries.CartQuery;
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

        [HttpGet]
        [Route("carts")]
        [Authorize]
        public async Task<IActionResult> GetCarts()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var claims = jwt.Claims.FirstOrDefault(c => c.Type == "UserId");
            var userId = claims.Value;
            return Ok(await _mediator.Send(new GetCart(Int32.Parse(userId))));
        }

        [HttpPost]
        [Route("carts")]
        [Authorize]
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
                return Ok(new { message = res });
            } return BadRequest(new { message = "Thêm sản phẩm vào giỏ hàng thất bại" });
        }

        [HttpPut]
        [Route("carts/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductToCart(int id, [FromBody] AddProductToCartDto product)
        {
            var res = await _mediator.Send(new UpdateCartCommand(id, product));
            if (res == "Cập nhật giỏ hàng thành công")
            {
                return Ok(new { message = res });
            }
            return BadRequest(new { message = "Sửa sản phẩm trong giỏ hàng thất bại" });
        }

        [HttpDelete]
        [Route("carts/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductToCart(int id)
        {
            var res = await _mediator.Send(new DeleteProductFromCartCommand(id));
            if (res == "Xóa sản phẩm khỏi giỏ hàng thành công")
            {
                return Ok(new { message = res });
            }
            return BadRequest(new { message = "Xóa sản phẩm khỏi giỏ hàng thất bại" });
        }
    }
}

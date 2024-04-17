using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command.OrderCommand;
using Sneaker_Be.Features.Queries.Order;
using System.IdentityModel.Tokens.Jwt;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        public OrderController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [Route("orders")]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var claims = jwt.Claims.FirstOrDefault(c => c.Type == "UserId");
            var userId = claims.Value;
            order.user_id = Int32.Parse(userId);
            var res = await _mediator.Send(new PostOrderCommand(order));
            if (res != 0) {
                return Ok(new {message = "Đặt hàng thành công", id = res});
            } return BadRequest(new { message = "Đặt hàng thất bại" });
        }

        [HttpGet]
        [Authorize]
        [Route("orders/user/{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var res = await _mediator.Send(new GetOrderDetail(id, Int32.Parse(userId)));
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(new { message = "Lấy thông tin đơn hàng thất bại" });
        }

        [HttpGet]
        [Authorize]
        [Route("orders/user")]
        public async Task<IActionResult> GetHistoryOrder()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            var res = await _mediator.Send(new GetHistoryOrder(Int32.Parse(userId)));
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(new { message = "Lấy lịch sử đơn hàng thất bại" });
        }
    }
}

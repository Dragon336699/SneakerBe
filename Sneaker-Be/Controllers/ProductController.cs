using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Sneaker_Be.Features.Queries;
using System.IdentityModel.Tokens.Jwt;

namespace Sneaker_Be.Controllers
{

    [Route("api/sneaker")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        public ProductController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _mediator.Send(new GetCategories()));
        }

        [HttpGet]
        [Route("products/all")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _mediator.Send(new GetAllProducts()));
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _mediator.Send(new GetProductById(id)));
        }

        [HttpGet]
        [Route("products/price")]
        public async Task<IActionResult> GetProductsViaPrice([FromQuery(Name = "min_price")] float minPrice, [FromQuery(Name = "max_price")] float maxPrice)
        {
            return Ok(await _mediator.Send(new GetProductViaPrice(minPrice, maxPrice)));
        }

        [HttpGet]
        [Route("products/search")]
        public async Task<IActionResult> SearchProducts([FromQuery(Name = "keyword")] string key)
        {
            return Ok(await _mediator.Send(new SearchProducts(key)));
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
    }
}

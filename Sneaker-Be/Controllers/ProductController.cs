using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Be.Features.Queries;
using Sneaker_Be.Features.Queries.ProductQuery;

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
        [Route("products/related/{id}")]
        public async Task<IActionResult> GetRelateProduct(int id)
        {
            return Ok(await _mediator.Send(new GetRelateProduct(id)));
        }
    }
}

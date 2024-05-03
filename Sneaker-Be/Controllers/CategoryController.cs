using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Command.CategoryCommand;
using Sneaker_Be.Features.Queries;
using Sneaker_Be.Features.Queries.CategoryQuery;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private IConfiguration _configuration;
        public CategoryController(IMediator mediator, IConfiguration configuration)
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
        [Route("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _mediator.Send(new GetCategoryById(id)));
        }

        [HttpPost]
        [Route("categories")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> PostCategory([FromBody] PostCategoryDto req)
        {
            var res = await _mediator.Send(new PostCategoryCommand(req.name));
            if (res != null)
            {
                return Ok(new { 
                    categories = res,
                    message = "Thêm danh mục mới thành công"
                });
            }
            return BadRequest(new
            {
                message = "Thêm danh mục mới thất bại"
            });
        }

        [HttpPut]
        [Route("categories/{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> UpdateCategory([FromBody] PostCategoryDto req, int id)
        {
            var res = await _mediator.Send(new UpdateCategoryCommand(id, req.name));
            if (res != null)
            {
                return Ok(new
                {
                    categories = res,
                    message = "Sửa danh mục thành công"
                });
            }
            return BadRequest(new
            {
                message = "Sửa danh mục thất bại"
            });
        }

        [HttpDelete]
        [Route("categories/{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var res = await _mediator.Send(new DeleteCategoryCommand(id));
            if (res != null)
            {
                return Ok(new
                {
                    categories = res,
                    message = "Xóa danh mục thành công"
                });
            }
            return BadRequest(new
            {
                message = "Xóa danh mục thất bại"
            });
        }
    }
}

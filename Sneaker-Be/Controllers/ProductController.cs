using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command.ProductCommand;
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
        private readonly IWebHostEnvironment _environment;
        public ProductController(IMediator mediator, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _configuration = configuration;
            _environment = environment;
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
        [Route("products/category/{id}")]
        public async Task<IActionResult> GetProductsByCategoryId(int id)
        {
            return Ok(await _mediator.Send(new GetProductsByCategoryId(id)));
        }

        [HttpGet]
        [Route("products/images/{imageName}")]
        public async Task<IActionResult> GetProductImage(string imageName)
        {
            var ImagePathOnServer = Path.Combine(_environment.ContentRootPath, "Upload", imageName);
            return PhysicalFile(ImagePathOnServer, "image/jpeg");
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

        [HttpPost]
        [Route("products")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> UploadProduct([FromBody] ProductDto product)
        {
            var res = await _mediator.Send(new UploadProductCommand(product));
            if (res > 0)
            {
                return Ok( new
                {
                    productId = res,
                    message = "Thêm sản phẩm thành công"
                });
            } return BadRequest(new
            {
                productId = 0,
                message = "Thêm sản phẩm thất bại"
            });
        }

        [HttpPut]
        [Route("products/{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product, int id)
        {
            var res = await _mediator.Send(new UpdateProductCommand(product, id));
            if (res)
            {
                return Ok(new
                {
                    message = "Sửa sản phẩm thành công"
                });
            }
            return BadRequest(new
            {
                message = "Sửa sản phẩm thất bại"
            });
        }

        [HttpDelete]
        [Route("products/{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var res = await _mediator.Send(new DeleteProductCommand(id));
            if (res)
            {
                return Ok(new
                {
                    message = "Xóa sản phẩm thành công"
                });
            }
            return BadRequest(new
            {
                message = "Xóa sản phẩm thất bại"
            });
        }

        [HttpPost]
        [Route("products/upload/{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> UploadPhoto([FromForm] List<IFormFile> files, int id)
        {
            var ImageFolder = Path.Combine(_environment.ContentRootPath, "Upload");
            const long maxSize = 10 * 1024 * 1024;
            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if ((fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg") && file.Length <= maxSize)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(ImageFolder, fileName);
                    using(FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                    }
                    try
                    {
                        await _mediator.Send(new UploadProductImageCommand(id, fileName));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new
                        {
                            message = "Thêm hình ảnh phụ cho sản phẩm thất bại"
                        });
                    }
                }
            }
            try
            {
                await _mediator.Send(new UpdateThumbnailProductCommand(id));
                return Ok(new
                {
                    message = "Thêm sản phẩm mới thành công"
                }) ;
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Thêm sản phẩm mới thất bại"
                });
            }
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;

namespace Sneaker_Be.Controllers
{
    [Route("api/sneaker")]
    [ApiController]
    public class SneakerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        public SneakerController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _mediator.Send(new GetCategories()));
        }

        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] User myUser)
        {
            var user = await _userManager.FindByNameAsync(myUser.PhoneNumber);
            return Ok(user);
        }
    }
}

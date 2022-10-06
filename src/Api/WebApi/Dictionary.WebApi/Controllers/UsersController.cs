using Dictionary.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserDetailsById([FromRoute]GetUserDetailQueryRequest request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{UserName}")]
        public async Task<IActionResult> GetUserDetailsByUserName([FromRoute] GetUserDetailQueryRequest request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}

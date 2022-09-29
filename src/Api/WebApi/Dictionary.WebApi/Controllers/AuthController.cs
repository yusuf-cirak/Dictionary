using Dictionary.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}

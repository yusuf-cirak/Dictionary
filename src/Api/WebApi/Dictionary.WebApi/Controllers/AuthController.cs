using Dictionary.Application.Features.Users.Commands.ConfirmEmail;
using Dictionary.Common.DTOs;
using Dictionary.Common.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommandRequest request)
        {
            bool response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmUserEmailCommandRequest request)
        {
            bool response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
        {
            Guid response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}

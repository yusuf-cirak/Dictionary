using Dictionary.Application.Features.Users.Commands.ConfirmEmail;
using Dictionary.Common.Features.Users.Commands.ChangePassword;
using Dictionary.Common.Features.Users.Commands.LoginUser;
using Dictionary.Common.Features.Users.Commands.Update;
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

        [HttpPut("password")]
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

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
        {
            Guid response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}

using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    public class BaseController:ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected Guid? UserId => new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}

using Dictionary.Common.Features.EntryFavorites.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class EntryFavoritesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEntryFavorite([FromBody] CreateEntryFavoriteCommandRequest request)
        {
            bool response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}

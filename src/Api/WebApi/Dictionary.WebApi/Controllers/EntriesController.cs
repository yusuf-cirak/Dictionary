using Dictionary.Common.DTOs.Entry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommandRequest request)
        {
            Guid entryId = await Mediator.Send(request);
            return Ok(entryId);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommandRequest request)
        {
            Guid entryId = await Mediator.Send(request);
            return Ok(entryId);
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> CreateEntryFavorite([FromBody] CreateEntryFavoriteCommandRequest request)
        {
            bool response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}

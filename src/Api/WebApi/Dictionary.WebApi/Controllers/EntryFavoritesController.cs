using Dictionary.Common.Features.EntryCommentFavorites.Commands.Create;
using Dictionary.Common.Features.EntryCommentFavorites.Commands.DeleteEntryCommentFavorite;
using Dictionary.Common.Features.EntryFavorites.Commands.Create;
using Dictionary.Common.Features.EntryFavorites.Commands.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class EntryFavoritesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEntryFavorite(Guid entryId, Guid? userId)
        {
            var request = new CreateEntryFavoriteCommandRequest(userId.Value, entryId);
            if (userId == Guid.Empty)
                userId = UserId;

            bool response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{entryId}")]
        public async Task<IActionResult> DeleteEntryFavorite([FromRoute] Guid entryId, Guid? userId)
        {
            var request = new DeleteEntryFavoriteCommandRequest { UserId = userId.Value,EntryId=entryId };
            if (userId == Guid.Empty)
                userId = UserId;

            bool response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> CreateEntryCommentFavorite(Guid entryCommentId, Guid userId)
        {
            var request = new CreateEntryCommentFavoriteCommandRequest(entryCommentId,userId);

            if (userId== Guid.Empty)
            {
                userId = UserId.Value;
            }

            bool response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("comment/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentFavorite([FromRoute] Guid entryCommentId, Guid userId)
        {
            var request = new DeleteEntryCommentFavoriteCommandRequest { EntryCommentId = entryCommentId, UserId = userId };

            if (userId == Guid.Empty)
            {
                userId = UserId.Value;
            }

            bool response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}

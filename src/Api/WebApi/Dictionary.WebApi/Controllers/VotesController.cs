using Dictionary.Common.Features.EntryCommentVotes.Commands.Create;
using Dictionary.Common.Features.EntryCommentVotes.Commands.Delete;
using Dictionary.Common.Features.EntryVotes.Commands.Create;
using Dictionary.Common.Features.EntryVotes.Commands.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class VotesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEntryVote([FromBody] CreateEntryVoteCommandRequest request)
        {
            if (request.UserId == Guid.Empty) request.UserId = UserId!.Value;

            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntryCommentVote([FromBody] CreateEntryCommentVoteCommandRequest request)
        {
            if (request.UserId == Guid.Empty) request.UserId = UserId!.Value;

            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("entry-vote")]
        public async Task<IActionResult> CreateEntryVote([FromBody] DeleteEntryVoteCommandRequest request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("entry-comment-vote")]
        public async Task<IActionResult> CreateEntryCommentVote([FromBody] DeleteEntryCommentVoteCommandRequest request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}

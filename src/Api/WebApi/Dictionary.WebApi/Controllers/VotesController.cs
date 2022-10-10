using AutoMapper.Configuration.Annotations;
using Dictionary.Common.Enums;
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
        [HttpPost("entry")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, Guid userId, VoteType voteType)
        {
            var request = new CreateEntryVoteCommandRequest { EntryId = entryId, VoteType = voteType, UserId = userId };

            if (request.UserId == Guid.Empty) request.UserId = UserId!.Value;

            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("entry-comment")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType, Guid userId)
        {
            var request = new CreateEntryCommentVoteCommandRequest { VoteType = voteType, EntryCommentId = entryCommentId, UserId = userId };

            if (request.UserId == Guid.Empty) request.UserId = UserId!.Value;

            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("entry-vote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote([FromRoute] Guid entryId, Guid userId)
        {
            var request = new DeleteEntryVoteCommandRequest(userId, entryId);

            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("entry-comment-vote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote([FromRoute] Guid entryCommentId, Guid userId)
        {
            var request = new DeleteEntryCommentVoteCommandRequest { EntryCommentId = entryCommentId, UserId = userId };

            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}

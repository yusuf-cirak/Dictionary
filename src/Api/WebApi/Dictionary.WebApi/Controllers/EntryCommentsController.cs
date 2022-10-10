using Dictionary.Application.Features.EntryComments.Queries.Get;
using Dictionary.Common.Features.EntryComments.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryCommentsController : BaseController
    {

        [HttpGet("{entryId}")]
        public async Task<IActionResult> GetEntryComments([FromRoute] Guid entryId,int page, int pageSize)
        {
            var request = new GetEntryCommentsQueryRequest {EntryId=entryId,UserId=UserId,BasePagingQuery=new(page,pageSize) };
            var response = await Mediator.Send(request);
            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommandRequest request)
        {
            Guid entryId = await Mediator.Send(request);
            return Ok(entryId);
        }
    }
}

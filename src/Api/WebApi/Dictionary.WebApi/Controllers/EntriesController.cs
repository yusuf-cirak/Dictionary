using Dictionary.Application.Features.Entries.Queries.GetEntries;
using Dictionary.Application.Features.Entries.Queries.GetEntryDetail;
using Dictionary.Application.Features.Entries.Queries.GetMainPageEntries;
using Dictionary.Common.Features.Entries.Commands.CreateEntry;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.Entries.Queries.GetUserEntries;
using Dictionary.Common.Features.Entries.Queries.Search;
using Dictionary.Common.Features.EntryComments.Commands.Create;
using Dictionary.Common.Features.EntryFavorites.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQueryRequest request)
        {
            GetEntriesQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserEntries(string userName, Guid? userId, int page, int pageSize)
        {
            if (!userId.HasValue && string.IsNullOrEmpty(userName))
            {
                userId = UserId;
            }
            var request = new GetUserEntriesQueryRequest(userId, userName, page, pageSize);

            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]SearchEntryQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetEntryDetailById([FromRoute] Guid id, int page, int pageSize)
        {
            var request = new GetEntryDetailQueryRequest { EntryId = id, UserId = UserId,PagingQuery=new(page,pageSize)};
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("main")] // main page entries randomly
        public async Task<IActionResult> GetMainPageEntries([FromQuery] GetMainPageEntriesQueryRequest request)
        {
            GetMainPageEntriesQueryResponse response = await Mediator.Send(request);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommandRequest request)
        {
            Guid entryId = await Mediator.Send(request);
            return Ok(entryId);
        }

    }
}

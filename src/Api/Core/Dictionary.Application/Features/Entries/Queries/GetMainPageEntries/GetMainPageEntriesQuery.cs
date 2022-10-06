using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Enums;
using Dictionary.Common.Extensions;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Application.Features.Entries.Queries.GetMainPageEntries
{
    public sealed class GetMainPageEntriesQueryRequest : BasePagingQuery, IRequest<GetMainPageEntriesQueryResponse>
    {
        public GetMainPageEntriesQueryRequest(int page, int pageSize) : base(page, pageSize)
        {
        }
        public Guid? UserId { get; }
    }

    public sealed class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQueryRequest, GetMainPageEntriesQueryResponse>
    {
        private readonly IEntryRepository _entryRepository;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<GetMainPageEntriesQueryResponse> Handle(GetMainPageEntriesQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();
            query = query.Include(e => e.EntryFavorites)
                .Include(e => e.User)
                .Include(e => e.EntryVotes);

            var list = query.Select(e => new GetEntryDetailViewModel
            {
                EntryId = e.Id,
                CreatedDate = e.CreateDate,
                Content = e.Content,
                Subject = e.Subject,
                UserName = e.User.UserName,
                FavoritedCount = e.EntryComments.Count,
                IsFavorited = request.UserId.HasValue && e.EntryFavorites.Any(ef => ef.UserId == request.UserId),
                VoteType = request.UserId.HasValue && e.EntryVotes.Any(ev => ev.UserId == request.UserId)
                    ? e.EntryVotes.FirstOrDefault(e => e.UserId == request.UserId)!.VoteType
                    : VoteType.None
            });

            var data = await list.GetPaged(request.Page, request.PageSize);


            var response = new GetMainPageEntriesQueryResponse { Results = data.Results, PageInfo = data.PageInfo };

            return response;
        }
    }

    public sealed class GetMainPageEntriesQueryResponse : PagedViewModel<GetEntryDetailViewModel>
    {

    }
}

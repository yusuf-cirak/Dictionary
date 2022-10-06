using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Entries.Queries.GetMainPageEntries;
using Dictionary.Common.Enums;
using Dictionary.Common.Extensions;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.EntryComments.Queries;
using Dictionary.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Application.Features.EntryComments.Queries.Get
{
    public sealed class GetEntryCommentsQueryRequest:IRequest<GetEntryCommentsQueryResponse>
    {
        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }

        public BasePagingQuery BasePagingQuery { get; set; }
    }

    public sealed class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQueryRequest, GetEntryCommentsQueryResponse>
    {
        private readonly IEntryCommentRepository _entryCommentRepository;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryRepository)
        {
            _entryCommentRepository = entryRepository;
        }

        public async Task<GetEntryCommentsQueryResponse> Handle(GetEntryCommentsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryCommentRepository.AsQueryable();
            query = query.Include(e => e.EntryCommentFavorites)
                .Include(e => e.User)
                .Include(e => e.EntryCommentVotes)
                .Where(e=>e.EntryId==request.EntryId);

            var list = query.Select(e => new GetEntryCommentsViewModel
            {
                CreatedDate = e.CreateDate,
                Content = e.Content,
                FavoritedCount = e.EntryCommentFavorites.Count,
                IsFavorited = request.UserId.HasValue && e.EntryCommentFavorites.Any(ef => ef.UserId == request.UserId),
                VoteType = request.UserId.HasValue && e.EntryCommentVotes.Any(ev => ev.UserId == request.UserId)
                    ? e.EntryCommentVotes.FirstOrDefault(e => e.UserId == request.UserId)!.VoteType
                    : VoteType.None
            });

            var data = await list.GetPaged(request.BasePagingQuery.Page, request.BasePagingQuery.PageSize);


            var response = new GetEntryCommentsQueryResponse { Results = data.Results, PageInfo = data.PageInfo };

            return response;
        }
    }

    public sealed class GetEntryCommentsQueryResponse:PagedViewModel<GetEntryCommentsViewModel>
    {
    }
}

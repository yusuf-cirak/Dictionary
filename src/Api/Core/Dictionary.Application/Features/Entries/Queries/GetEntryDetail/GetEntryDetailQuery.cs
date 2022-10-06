using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Entries.Queries.GetMainPageEntries;
using Dictionary.Common.Enums;
using Dictionary.Common.Extensions;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Application.Features.Entries.Queries.GetEntryDetail
{
    public sealed class GetEntryDetailQueryRequest:IRequest<GetEntryDetailQueryResponse>
    {
        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }
        public BasePagingQuery PagingQuery { get; set; }

    }

    public sealed class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQueryRequest, GetEntryDetailQueryResponse>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<GetEntryDetailQueryResponse> Handle(GetEntryDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();
            query = query.Include(e => e.EntryFavorites)
                .Include(e => e.User)
                .Include(e => e.EntryVotes)
                .Where(e=>e.Id==request.EntryId);

            var list = query.Select(e => new GetEntryDetailViewModel
            {
                EntryId = e.Id,
                CreatedDate = e.CreateDate,
                Content = e.Content,
                Subject = e.Subject,
                UserName = e.User.UserName,
                FavoritedCount = e.EntryFavorites.Count,
                IsFavorited = request.UserId.HasValue && e.EntryFavorites.Any(ef => ef.UserId == request.UserId),
                VoteType = request.UserId.HasValue && e.EntryVotes.Any(ev => ev.UserId == request.UserId)
                    ? e.EntryVotes.FirstOrDefault(e => e.UserId == request.UserId)!.VoteType
                    : VoteType.None
            });


            var response = await list.ProjectTo<GetEntryDetailQueryResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return response!;
        }
    }

    public sealed class GetEntryDetailQueryResponse:GetEntryDetailViewModel
    {
    }
}

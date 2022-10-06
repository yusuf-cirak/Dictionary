using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Extensions;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.Entries.Queries.GetUserEntries;
using Dictionary.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Application.Features.Entries.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQueryRequest, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            if (request.UserId != Guid.Empty)
            {
                query = query.Where(e => e.UserId == request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(e => e.User.UserName == request.UserName);
            }
            else return null;

            query = query.Include(e => e.User);
            query = query.Include(e => e.EntryFavorites);

            var list = query.Select(e => new GetUserEntriesDetailViewModel
            {
                Id = e.Id,
                Content = e.Content,
                CreatedByUserName = e.User.UserName,
                CreatedDate = e.CreateDate,
                FavoritedCount = e.EntryFavorites.Count,
                IsFavorited = false,
                Subject = e.Subject
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);
            return entries;
        }
    }
}

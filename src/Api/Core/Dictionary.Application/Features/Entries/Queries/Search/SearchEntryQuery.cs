using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.Entries.Queries.Search;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Application.Features.Entries.Queries.Search
{

    public sealed class SearchEntryQueryHandler : IRequestHandler<SearchEntryQueryRequest, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQueryRequest request, CancellationToken cancellationToken)
        {
            return await _entryRepository.GetAsync(e => EF.Functions.Like(e.Subject, $"{request.SearchText}%")).Select(e=>new SearchEntryViewModel
            {
                Id=e.Id,
                Subject=e.Subject
            }).ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Features.Entries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Application.Features.Entries.Queries.GetEntries
{
    public sealed class GetEntriesQueryRequest : IRequest<GetEntriesQueryResponse>
    {
        public bool GetTodayEntries { get; set; } // Will we fetch today's entries from db?
        public int Count { get; set; } = 100; // How much entry to fetch from db?
    }

    public sealed class GetEntriesQueryHandler : IRequestHandler<GetEntriesQueryRequest, GetEntriesQueryResponse>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<GetEntriesQueryResponse> Handle(GetEntriesQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            if (request.GetTodayEntries)
            {

               query = query.Where(e => e.CreateDate >= DateTime.Now.Date && 
                                             e.CreateDate <= DateTime.Now.AddDays(1).Date);
                // DateTime.Now.Date is = 01.10.2022 00:00
                // DateTime.Now.AddDays(1).Date is = 02.10.2022 00:00
            }

            query = query.Include(e => e.EntryComments)
                 .OrderBy(e => Guid.NewGuid())
                 .Take(request.Count);



            var response = new GetEntriesQueryResponse
            {
                EntriesList = await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };

            return response;
        }
    }

    public sealed class GetEntriesQueryResponse
    {
        public List<GetEntriesViewModel> EntriesList { get; set; }
    }
}

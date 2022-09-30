using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Entries.Rules;
using Dictionary.Common.Features.Entries.Commands.CreateEntry;
using Dictionary.Domain.Entities;
using MediatR;

namespace Dictionary.Application.Features.Entries.Commands.CreateEntry
{
    public sealed class CreateEntryCommandHandler:IRequestHandler<CreateEntryCommandRequest,Guid>
    {
        private readonly IEntryRepository _repository;
        private readonly IMapper _mapper;
        private readonly EntryBusinessRules _businessRules;

        public CreateEntryCommandHandler(IEntryRepository repository, IMapper mapper, EntryBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<Guid> Handle(CreateEntryCommandRequest request, CancellationToken cancellationToken)
        {
           //await _businessRules.CheckUserCredentialBeforeEntryCreated(request.CreatedByUserId);

            var entry = _mapper.Map<Entry>(request);

             await _repository.AddAsync(entry);

             return entry.Id;
        }
    }
}

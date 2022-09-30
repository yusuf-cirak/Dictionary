using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Features.EntryComments.Commands.Create;
using Dictionary.Domain.Entities;
using MediatR;

namespace Dictionary.Application.Features.EntryComments.Commands.Create
{
    public sealed class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommandRequest, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEntryCommentRepository _repository;

        public CreateEntryCommentCommandHandler(IMapper mapper, IEntryCommentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateEntryCommentCommandRequest request, CancellationToken cancellationToken)
        {
            var entryComment = _mapper.Map<EntryComment>(request);
            await _repository.AddAsync(entryComment);

            return entryComment.Id;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Features.EntryVotes.Commands.Create;
using Dictionary.Common.Infrastructure;
using Dictionary.Domain.Entities;
using MediatR;

namespace Dictionary.Application.Features.EntryVotes.Commands.Create
{
    public sealed class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommandRequest, bool>
    {
        private readonly IEntryVoteRepository _repository;
        private readonly IMapper _mapper;
        public async Task<bool> Handle(CreateEntryVoteCommandRequest request, CancellationToken cancellationToken)
        {
            // var entryVote = _mapper.Map<EntryVote>(request);
            //await _repository.AddAsync(entryVote);

            var @event = new CreateEntryVoteEvent { EntryId = request.EntryId, UserId = request.UserId, VoteType = request.VoteType };

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.CreateEntryVoteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

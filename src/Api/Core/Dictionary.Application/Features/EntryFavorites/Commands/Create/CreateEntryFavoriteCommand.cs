using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Features.EntryFavorites.Commands.Create;
using Dictionary.Common.Infrastructure;
using Dictionary.Domain.Entities;
using MediatR;

namespace Dictionary.Application.Features.EntryFavorites.Commands.Create
{
    public sealed class CreateEntryFavoriteCommand : IRequestHandler<CreateEntryFavoriteCommandRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly IEntryFavoriteRepository _repository;

        public CreateEntryFavoriteCommand(IMapper mapper, IEntryFavoriteRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> Handle(CreateEntryFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            // var entryFavorite = _mapper.Map<EntryFavorite>(request);

            //await _repository.AddAsync(entryFavorite);

            // return entryFavorite.Id;

            var @event = new CreateEntryFavoriteEvent(userId: request.UserId, entryId: request.EntryId);

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavoriteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.DeleteEntryFavoriteQueue,
                obj: @event);

            return await Task.FromResult(true);

        }
    }
}

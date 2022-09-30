using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Features.EntryFavorites.Commands.Delete;
using Dictionary.Common.Infrastructure;
using MediatR;

namespace Dictionary.Application.Features.EntryFavorites.Commands.Delete
{
    public sealed class DeleteEntryFavoriteCommandHandler : IRequestHandler<DeleteEntryFavoriteCommandRequest, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new DeleteEntryFavoriteEvent(userId: request.UserId, entryId: request.EntryId);

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavoriteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.CreateEntryFavoriteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

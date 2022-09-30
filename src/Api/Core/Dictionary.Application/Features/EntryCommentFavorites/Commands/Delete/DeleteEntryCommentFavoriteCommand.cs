using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Features.EntryCommentFavorites.Commands.DeleteEntryCommentFavorite;
using Dictionary.Common.Infrastructure;
using MediatR;

namespace Dictionary.Application.Features.EntryCommentFavorites.Commands.Delete
{
    public sealed class DeleteEntryCommentFavoriteCommandHandler : IRequestHandler<DeleteEntryCommentFavoriteCommandRequest, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new DeleteEntryCommentFavoriteEvent
            { UserId = request.UserId, EntryCommentId = request.EntryCommentId };

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.FavoriteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.DeleteEntryCommentFavoriteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

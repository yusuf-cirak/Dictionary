using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;
using MediatR;

namespace Dictionary.Application.Features.EntryComments.Commands.CreateEntryCommentFavorite
{
    public sealed class CreateEntryCommentFavoriteCommandRequest:IRequest<bool>
    {
        public Guid EntryCommentId { get; }
        public Guid UserId { get; }

        public CreateEntryCommentFavoriteCommandRequest(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }

    public sealed class CreateEntryCommandFavCommandHandler:IRequestHandler<CreateEntryCommentFavoriteCommandRequest,bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new CreateEntryCommentFavoriteEvent { UserId = request.UserId,EntryCommentId = request.EntryCommentId};

            QueueFactory.SendMessageToExchange(exchangeName:DictionaryConstants.FavoriteExchangeName,
                exchangeType:DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.CreateEntryCommentFavoriteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

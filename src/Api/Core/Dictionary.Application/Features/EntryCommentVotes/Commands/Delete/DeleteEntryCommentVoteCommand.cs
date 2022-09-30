using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Features.EntryCommentVotes.Commands.Delete;
using Dictionary.Common.Infrastructure;
using MediatR;

namespace Dictionary.Application.Features.EntryCommentVotes.Commands.Delete
{
    public sealed class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommandRequest, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new DeleteEntryCommentVoteEvent
            { UserId = request.UserId, EntryCommentId = request.EntryCommentId };

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.DeleteEntryCommentVoteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

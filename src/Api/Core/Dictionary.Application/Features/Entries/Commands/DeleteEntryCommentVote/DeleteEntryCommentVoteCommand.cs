using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common;
using Dictionary.Common.DTOs.Entry;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;
using MediatR;

namespace Dictionary.Application.Features.Entries.Commands.DeleteEntryCommentVote
{
    public sealed class DeleteEntryCommentVoteCommandHandler:IRequestHandler<DeleteEntryCommentVoteCommandRequest,bool>
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

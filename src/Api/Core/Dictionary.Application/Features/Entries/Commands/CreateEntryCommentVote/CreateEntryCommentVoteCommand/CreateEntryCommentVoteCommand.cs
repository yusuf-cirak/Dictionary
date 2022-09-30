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

namespace Dictionary.Application.Features.Entries.Commands.CreateEntryCommentVote.CreateEntryCommentVoteCommand
{
    public sealed class CreateEntryCommentVoteCommandHandler:IRequestHandler<CreateEntryCommentVoteCommandRequest,bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new CreateEntryCommentVoteEvent{ EntryCommentId = request.EntryCommentId, UserId = request.UserId, VoteType = request.VoteType };

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.CreateEntryCommentVoteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

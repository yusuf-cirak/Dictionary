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

namespace Dictionary.Application.Features.Entries.Commands.DeleteEntryVote.DeleteEntryVoteCommand
{
    public sealed class DeleteEntryVoteCommandHandler:IRequestHandler<Common.DTOs.Entry.DeleteEntryVoteCommandRequest,bool>
    {
        public async Task<bool> Handle(Common.DTOs.Entry.DeleteEntryVoteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new DeleteEntryVoteEvent{EntryId = request.EntryId,UserId = request.UserId};

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.DeleteEntryVoteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common;
using Dictionary.Common.Events;
using Dictionary.Common.Features.EntryVotes.Commands.Delete;
using Dictionary.Common.Infrastructure;
using MediatR;

namespace Dictionary.Application.Features.EntryVotes.Commands.Delete
{
    public sealed class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommandRequest, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommandRequest request, CancellationToken cancellationToken)
        {
            var @event = new DeleteEntryVoteEvent { EntryId = request.EntryId, UserId = request.UserId };

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.VoteExchangeName,
                exchangeType: DictionaryConstants.DefaultExchangeType,
                queueName: DictionaryConstants.DeleteEntryVoteQueue,
                obj: @event);

            return await Task.FromResult(true);
        }
    }
}

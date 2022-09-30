using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.Features.EntryCommentVotes.Commands.Delete
{
    public sealed class DeleteEntryCommentVoteCommandRequest : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}

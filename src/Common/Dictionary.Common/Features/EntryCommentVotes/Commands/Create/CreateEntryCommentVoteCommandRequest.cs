using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.Enums;
using MediatR;

namespace Dictionary.Common.Features.EntryCommentVotes.Commands.Create
{
    public class CreateEntryCommentVoteCommandRequest : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
    }
}

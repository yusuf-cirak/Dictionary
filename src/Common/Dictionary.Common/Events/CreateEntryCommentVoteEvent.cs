using Dictionary.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Events
{
    public sealed class CreateEntryCommentVoteEvent
    {
        public Guid UserId { get; set; }
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
    }
}

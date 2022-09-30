using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.Enums;

namespace Dictionary.Common.Events
{
    public sealed class CreateEntryVoteEvent
    {
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid EntryId { get; set; }
    }
}

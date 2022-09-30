using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.Enums;
using MediatR;

namespace Dictionary.Common.DTOs.Entry
{
    public class CreateEntryVoteCommandRequest:IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }

    }
}

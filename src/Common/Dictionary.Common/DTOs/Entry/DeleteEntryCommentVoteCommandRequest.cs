using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.DTOs.Entry
{
    public sealed class DeleteEntryCommentVoteCommandRequest:IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}

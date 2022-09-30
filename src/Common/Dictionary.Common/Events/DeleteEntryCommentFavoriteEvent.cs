using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Events
{
    public sealed class DeleteEntryCommentFavoriteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}

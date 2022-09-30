using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Events
{
    public sealed class DeleteEntryFavoriteEvent
    {
        public DeleteEntryFavoriteEvent(Guid userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }

        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Events
{
    public sealed class CreateEntryFavoriteEvent
    {
        public Guid UserId { get; }
        public Guid EntryId { get; }

        public CreateEntryFavoriteEvent(Guid userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}

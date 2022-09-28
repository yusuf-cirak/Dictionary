using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities.Common;

namespace Dictionary.Domain.Entities
{
    public class Entry:BaseEntity
    {
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<EntryComment> EntryComments { get; set; }
        public virtual ICollection<EntryVote> EntryVotes { get; set; }
        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
    }
}

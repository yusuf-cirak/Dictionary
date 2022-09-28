using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities.Common;

namespace Dictionary.Domain.Entities
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string Password  { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<EntryComment> EntryComments { get; set; }

        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    }
}

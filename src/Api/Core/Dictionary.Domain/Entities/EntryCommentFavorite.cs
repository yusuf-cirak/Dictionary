using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Domain.Entities
{
    public class EntryCommentFavorite
    {
        public Guid UserId { get; set; }
        public Guid EntryCommentId { get; set; }
        public virtual EntryComment EntryComment { get; set; }
        public virtual User User { get; set; }
    }
}

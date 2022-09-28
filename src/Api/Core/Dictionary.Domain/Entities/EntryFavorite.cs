using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities.Common;

namespace Dictionary.Domain.Entities
{
    public class EntryFavorite:BaseEntity
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public virtual Entry Entry { get; set; }
        public virtual User User { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Features.Entries.Models
{
    public class GetEntryDetailViewModel:BaseFooterRateFavoritedViewModel
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

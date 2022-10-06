using Dictionary.Common.Features.Entries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Features.EntryComments.Queries
{
    public class GetEntryCommentsViewModel:BaseFooterRateFavoritedViewModel
    {
        // Get entry rate for user (if logined user will see voteType) from BaseFooterRateFavoritedViewModel
        // Get favorite count from BaseFooterRateFavoritedViewModel
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }

        // This class returns entryComments with entryComment favorites, votes.

    }
}

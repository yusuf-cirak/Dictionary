using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.Enums;

namespace Dictionary.Common.Features.Entries.Models
{
    public class BaseFooterViewModel
    {
        // Proccesses about entry rating
        public VoteType VoteType { get; set; }
    }

    public class BaseFooterFavoritedViewModel
    {
        // Entry favorite informations
        public bool IsFavorited { get; set; }
        public int FavoritedCount { get; set; }

    }

    public class BaseFooterRateFavoritedViewModel: BaseFooterFavoritedViewModel
    {
        // Entry favorite and rate informations in one class
        public VoteType VoteType { get; set; }
    }
}

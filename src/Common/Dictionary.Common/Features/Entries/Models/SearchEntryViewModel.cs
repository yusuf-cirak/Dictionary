using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Features.Entries.Models
{
    public class SearchEntryViewModel
    {
        // This is a searched entry view model
        public Guid Id { get; set; } // Route to subjects with id
        public string Subject { get; set; } // Searched subject name
    }
}

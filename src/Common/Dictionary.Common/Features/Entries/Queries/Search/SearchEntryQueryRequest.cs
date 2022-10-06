using Dictionary.Common.Features.Entries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Features.Entries.Queries.Search
{
    public sealed class SearchEntryQueryRequest : IRequest<List<SearchEntryViewModel>>
    {
        public string SearchText { get; set; }
    }
}

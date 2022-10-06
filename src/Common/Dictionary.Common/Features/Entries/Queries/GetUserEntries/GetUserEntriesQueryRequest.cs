using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Features.Entries.Queries.GetUserEntries
{
    public class GetUserEntriesQueryRequest:BasePagingQuery,IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }

        public GetUserEntriesQueryRequest(Guid? userId, string userName,int page=1, int pageSize=10):base(page,pageSize)
        {
            UserId = userId;
            UserName = userName;
            Page = page;
            PageSize = pageSize;
        }
    }
}

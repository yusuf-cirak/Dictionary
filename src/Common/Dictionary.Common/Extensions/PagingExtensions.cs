using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Common.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PagedViewModel<T>> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T:class
        {
            var count = await query.CountAsync();

            var paging = new Page(currentPage, pageSize,count);

            var data = await query.Skip(paging.Skip).Take(paging.PageSize).AsNoTracking().ToListAsync();

            var list = new PagedViewModel<T> { Results = data, PageInfo = paging };

            return list;
        }
    }
}

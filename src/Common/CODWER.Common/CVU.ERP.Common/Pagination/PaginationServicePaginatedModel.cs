using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CVU.ERP.Common.Pagination
{
    public partial class PaginationService
    {
        ///<summary>
        ///Return PaginatedList (created for best performance)
        ///</summary>
        public async Task<PaginatedList<TDestination>> Create<TSource, TDestination>(IQueryable<TSource> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var mappedItems = _mapper.Map<List<TDestination>>(items).ToList();

            return new PaginatedList<TDestination>(mappedItems, count, pageNumber, pageSize);
        }
    }
}

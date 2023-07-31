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
            var count = source.ToArray().Count();
            var skipCount = (pageNumber - 1) * pageSize;

            var items = (source.ToArray().Skip(skipCount).Take(pageSize)).ToList();
            var mappedItems = _mapper.Map<List<TDestination>>(items).ToList();

            return new PaginatedList<TDestination>(mappedItems, count, pageNumber, pageSize);
        }

        public async Task<PaginatedList<TDestination>> CreatePage<TSource, TDestination>(IQueryable<TSource> source, int pageNumber, int pageSize, int count)
        {
            var mappedItems = _mapper.Map<List<TDestination>>(source).ToList();

            return new PaginatedList<TDestination>(mappedItems, count, pageNumber, pageSize);
        }
    }
}

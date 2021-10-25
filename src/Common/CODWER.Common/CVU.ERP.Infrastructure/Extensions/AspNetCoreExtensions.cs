using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CVU.ERP.Infrastructure.Extensions
{
    public static class PagedListExtensions
    {
        public static void AddPagination(this IHeaderDictionary headers, PaginatedHeaderParameter pagedSummary)
        {
            headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedSummary));
        }
    }
}
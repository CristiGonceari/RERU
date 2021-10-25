using System.Collections.Generic;

namespace CVU.ERP.Common.Pagination
{
    public class PaginatedModel<T>
    {
        public PaginatedModel(BasePagedList<T> list)
        {
            Items = list;
            PagedSummary = list.PagedSummary;
        }
        public IEnumerable<T> Items { get; set; }
        public PaginatedHeaderParameter PagedSummary { get; set; }
    }
}

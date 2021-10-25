using System;

namespace CVU.ERP.Common.Pagination
{
    public class PaginatedHeaderParameter
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
    }
}
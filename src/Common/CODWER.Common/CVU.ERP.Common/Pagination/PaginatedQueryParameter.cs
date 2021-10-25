namespace CVU.ERP.Common.Pagination
{
    public class PaginatedQueryParameter
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;
        public int Page { get; set; } = 1;
        public int ItemsPerPage
        {
            get => this._pageSize;

            set => this._pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public string OrderField { get; set; }
        public string Fields { get; set; }
        public string SearchBy { get; set; }
    }
}
using System;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Logging.Application.GetLoggingValuesQuery
{
    public class GetLoggingValuesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LogDto>>
    {
        public string Event { get; set; }
        public string ProjectName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}

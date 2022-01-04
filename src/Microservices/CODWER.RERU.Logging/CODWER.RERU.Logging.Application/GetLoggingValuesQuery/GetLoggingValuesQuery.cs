using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Logging.Application.GetLoggingValuesQuery
{
    public class GetLoggingValuesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LogDto>>
    {
        public string Event { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string UserIdentifier { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}

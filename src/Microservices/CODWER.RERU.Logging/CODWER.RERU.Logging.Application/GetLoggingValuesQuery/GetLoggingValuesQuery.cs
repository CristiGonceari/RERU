using CODWER.RERU.Logging.Application.Permissions;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System;

namespace CODWER.RERU.Logging.Application.GetLoggingValuesQuery
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DATE_JURNALIZATE)]

    public class GetLoggingValuesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LogDto>>
    {
        public string Event { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string UserIdentifier { get; set; }
        public string EventMessage { get; set; }
        public string JsonMessage { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

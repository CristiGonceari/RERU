using System;
using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.GetTimeSheetTableValues
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_TABELA_DE_PONTAJ)]
    public class GetTimeSheetTableValuesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ContractorTimeSheetTableDto>>
    {
        public string ContractorName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}

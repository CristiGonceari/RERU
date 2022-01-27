using System;
using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorTimeSheetTable
{
    [ModuleOperation(permission: PermissionCodes.TIME_SHEET_TABLE_GENERAL_ACCESS)]
    public class GetTimeSheetTableQuery : IRequest<ContractorProfileTimeSheetTableDto>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

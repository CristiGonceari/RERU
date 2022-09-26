using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.AddTimeSheetTableValue
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_TABELA_DE_PONTAJ)]
    public class AddEditTimeSheetTableCommand : IRequest<int>
    {
        public AddEditTimeSheetTableDto Data { get; set; }
    }
}

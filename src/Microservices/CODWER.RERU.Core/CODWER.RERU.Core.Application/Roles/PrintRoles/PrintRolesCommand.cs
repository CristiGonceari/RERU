using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.PrintRoles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ROLURI)]
    public class PrintRolesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}

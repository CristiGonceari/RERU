using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.EditModuleRole
{
    [ModuleOperation(permission: PermissionCodes.ACTUALIZAREA_ROLULUI_LA_MODUL)]

    public class EditModuleRoleCommand : IRequest<Unit>
    {
        public EditModuleRoleCommand(AddEditModuleRoleDto moduleRole)
        {
            ModuleRole = moduleRole;
        }

        public AddEditModuleRoleDto ModuleRole { set; get; }
    }
}
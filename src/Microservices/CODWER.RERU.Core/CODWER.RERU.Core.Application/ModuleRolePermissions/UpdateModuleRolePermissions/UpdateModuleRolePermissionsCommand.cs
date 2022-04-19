using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRolePermissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.UpdateModuleRolePermissions
{
    [ModuleOperation(permission: PermissionCodes.ACTUALIZAREA_PERMISIUNILOR_ROLULUI_LA_MODUL)]

    public class UpdateModuleRolePermissionsCommand : IRequest<Unit>
    {
        public int id;
        public UpdateModuleRolePermissionsCommand(ModuleRolePermissionsDto moduleRolePermissions)
        {
            ModuleRolePermissions = moduleRolePermissions;
        }

        public ModuleRolePermissionsDto ModuleRolePermissions { set; get; }
    }
}
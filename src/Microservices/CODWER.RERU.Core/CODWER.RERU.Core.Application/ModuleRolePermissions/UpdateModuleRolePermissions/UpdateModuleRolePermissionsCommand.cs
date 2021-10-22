
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.ModuleRolePermissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.UpdateModuleRolePermissions
{
    [ModuleOperation(permission: Permissions.UPDATE_MODULE_ROLE_PERMISSIONS)]

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
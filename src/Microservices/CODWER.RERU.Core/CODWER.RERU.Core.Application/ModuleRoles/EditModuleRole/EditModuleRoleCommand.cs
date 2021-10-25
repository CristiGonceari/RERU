
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.EditModuleRole
{
    [ModuleOperation(permission: Permissions.UPDATE_MODULE_ROLE)]

    public class EditModuleRoleCommand : IRequest<Unit>
    {
        public EditModuleRoleCommand(AddEditModuleRoleDto moduleRole)
        {
            ModuleRole = moduleRole;
        }

        public AddEditModuleRoleDto ModuleRole { set; get; }
    }
}
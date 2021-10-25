
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole
{
    [ModuleOperation(permission: Permissions.ADD_MODULE_ROLE)]

    public class CreateModuleRoleCommand : IRequest<Unit>
    {
        public CreateModuleRoleCommand(AddEditModuleRoleDto moduleRole)
        {
            ModuleRole = moduleRole;
        }

        public AddEditModuleRoleDto ModuleRole { set; get; }
    }
}
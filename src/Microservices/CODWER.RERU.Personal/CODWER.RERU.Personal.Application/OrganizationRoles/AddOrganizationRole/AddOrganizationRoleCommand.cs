using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATION_ROLES_GENERAL_ACCESS)]

    public class AddRoleCommand : IRequest<int>
    {
        public AddEditRoleDto Data { get; set; }
    }
}

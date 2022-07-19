using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRole
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATION_ROLES_GENERAL_ACCESS)]

    public class GetRoleQuery : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}

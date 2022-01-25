using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRole
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATION_ROLES_GENERAL_ACCESS)]

    public class GetOrganizationRoleQuery : IRequest<OrganizationRoleDto>
    {
        public int Id { get; set; }
    }
}

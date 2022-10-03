using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRole
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ROLURI)]

    public class GetRoleQuery : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}

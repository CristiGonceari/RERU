using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.RemoveOrganizationRole
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ROLURI)]

    public class RemoveRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

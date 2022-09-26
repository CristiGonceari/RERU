using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.UpdateOrganizationRole
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ROLURI)]

    public class UpdateRoleCommand : IRequest<Unit>
    {
        public AddEditRoleDto Data { get; set; }
    }
}

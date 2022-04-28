using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.GetRole
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ROLURI)]
    public class GetRoleQuery : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}

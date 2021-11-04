using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleDetails
{
    [ModuleOperation(permission: PermissionCodes.VIEW_ROLE_PERMISSIONS)]

    public class GetModuleRoleDetailsQuery : IRequest<ModuleRoleDto>
    {
        public GetModuleRoleDetailsQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
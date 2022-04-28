using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.GetRoles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ROLURI)]
    public class GetRolesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RoleDto>>
    {
        public string Name { get; set; }
    }
}

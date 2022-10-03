using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRoles
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ROLURI)]

    public class GetRolesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RoleDto>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortCode { get; set; }

        public string SearchWord { get; set; }
    }
}

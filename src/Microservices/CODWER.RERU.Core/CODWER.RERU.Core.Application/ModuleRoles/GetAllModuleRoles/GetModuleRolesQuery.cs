using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetAllModuleRoles
{
    public class GetModuleRolesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModuleRoleRowDto>>
    {
        public int ModuleId { get; set; }
    }
}
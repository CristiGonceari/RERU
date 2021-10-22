using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using MediatR;

namespace CODWER.RERU.Core.Application.ModulePermissions.GetAllModulePermissions
{
    public class GetModulePermissionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModulePermissionRowDto>>
    {
        public int ModuleId { get; set; }
        public string Keyword { get; set; }
    }
}
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using MediatR;

namespace CODWER.RERU.Core.Application.ModulePermissions.GetModulePermissions
{
    public class GetModulePermissionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModulePermissionRowDto>>
    {
        public int ModuleId { get; set; }
        public string Code { set; get; }
        public string Description { set; get; }
    }
}
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissions
{
    class GetModuleRolePermissionsQueryHandler : BaseHandler, IRequestHandler<GetModuleRolePermissionsQuery, PaginatedModel<ModulePermissionRowDto>>
    {

        private readonly IPaginationService _paginationService;

        public GetModuleRolePermissionsQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ModulePermissionRowDto>> Handle(GetModuleRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            var moduleRolePermissions = CoreDbContext.ModuleRoles.Where(m => m.Id == request.RoleId).SelectMany(rp=>rp.Permissions.Select(mrp=>mrp.Permission));

            var paginatedModel = _paginationService.MapAndPaginateModel<Data.Entities.ModulePermission, ModulePermissionRowDto>(moduleRolePermissions, request);

            return paginatedModel;
        }
    }
}
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.ModulePermissions.GetModulePermissions
{
    public class GetModulePermissionsQueryHandler : BaseHandler, IRequestHandler<GetModulePermissionsQuery, PaginatedModel<ModulePermissionRowDto>>
    {

        private readonly IPaginationService _paginationService;

        public GetModulePermissionsQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ModulePermissionRowDto>> Handle(GetModulePermissionsQuery request, CancellationToken cancellationToken)
        {
            var moduleRoles = AppDbContext.ModulePermissions.Where(m => m.ModuleId == request.ModuleId);

            moduleRoles = GetAndFilterModulePermissions.Filter(moduleRoles, request.Code, request.Description);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ModulePermission, ModulePermissionRowDto>(moduleRoles, request);

            return paginatedModel;
        }
    }
}
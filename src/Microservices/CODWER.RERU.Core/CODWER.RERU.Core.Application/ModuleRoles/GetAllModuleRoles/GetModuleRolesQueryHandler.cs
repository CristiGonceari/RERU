using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetAllModuleRoles
{
    public class GetModuleRolesQueryHandler : BaseHandler, IRequestHandler<GetModuleRolesQuery, PaginatedModel<ModuleRoleRowDto>>
    {

        private readonly IPaginationService _paginationService;

        public GetModuleRolesQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ModuleRoleRowDto>> Handle(GetModuleRolesQuery request, CancellationToken cancellationToken)
        {
            var moduleRoles = CoreDbContext.ModuleRoles
                .Where(m=>m.ModuleId == request.ModuleId);

            var paginatedModel = _paginationService.MapAndPaginateModel<Data.Entities.ModuleRole, ModuleRoleRowDto>(moduleRoles, request);

            return paginatedModel;
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Core.Data.Entities;

namespace CODWER.RERU.Core.Application.Modules.GetAllModules {
    public class GetAllModulesQueryHandler : BaseHandler, IRequestHandler<GetAllModulesQuery, PaginatedModel<ModuleDto>> 
    {

        private readonly IPaginationService _paginationService;

        public GetAllModulesQueryHandler (ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base (commonServiceProvider) {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ModuleDto>> Handle (GetAllModulesQuery request, CancellationToken cancellationToken) {
            var modules = CoreDbContext.Modules.AsQueryable ();

            if (request != null && !string.IsNullOrEmpty (request.Keyword)) {
                modules = modules.Where (x => EF.Functions.Like (x.Name, $"%{request.Keyword}%"));
            }

            var paginatedModel = _paginationService.MapAndPaginateModel<Data.Entities.Module, ModuleDto> (modules.OrderByDescending(m=>m.Priority), request);

            return paginatedModel;
        }
    }
}
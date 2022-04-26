using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Modules.GetAllModules {
    public class GetAllModulesQueryHandler : BaseHandler, IRequestHandler<GetAllModulesQuery, PaginatedModel<ModuleDto>> 
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetAllModulesQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService, AppDbContext appDbContext) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<ModuleDto>> Handle (GetAllModulesQuery request, CancellationToken cancellationToken) 
        {
            var modules = GetAndPrintModules.Filter(_appDbContext, request.Keyword);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<global::RERU.Data.Entities.Module, ModuleDto> (modules.OrderByDescending(m=>m.Priority), request);

            return paginatedModel;
        }
    }
}
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlansByDate
{
    public class GetPlansByDateQueryHandler : IRequestHandler<GetPlansByDateQuery, PaginatedModel<PlanDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetPlansByDateQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<PlanDto>> Handle(GetPlansByDateQuery request, CancellationToken cancellationToken)
        {

            var plans = _appDbContext.Plans.Where(p => p.FromDate.Date <= request.Date && p.TillDate.Date >= request.Date)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<Plan, PlanDto>(plans, request);
        }
    }
}

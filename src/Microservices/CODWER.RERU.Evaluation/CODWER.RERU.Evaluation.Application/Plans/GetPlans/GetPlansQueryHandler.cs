using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlans
{
    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, PaginatedModel<PlanDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetPlansQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<PlanDto>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = GetAndFilterPlans.Filter(_appDbContext, request.Name, request.FromDate, request.TillDate);

            return await _paginationService.MapAndPaginateModelAsync<Plan, PlanDto>(plans, request);
        }
    }

}

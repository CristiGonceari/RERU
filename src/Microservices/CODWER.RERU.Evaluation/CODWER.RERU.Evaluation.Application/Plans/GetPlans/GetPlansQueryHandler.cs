﻿using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var plans = _appDbContext.Plans.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                plans = plans.Where(x => x.Name.Contains(request.Name));
            }

            if (request.FromDate != null && request.TillDate != null)
            {
                plans = plans.Where(p => p.FromDate.Date >= request.FromDate && p.TillDate.Date <= request.TillDate ||
                                                    (request.FromDate <= p.FromDate.Date && p.FromDate.Date <= request.TillDate) && (request.FromDate <= p.TillDate.Date && p.TillDate.Date >= request.TillDate) ||
                                                    (request.FromDate >= p.FromDate.Date && p.FromDate.Date <= request.TillDate) && (request.FromDate <= p.TillDate.Date && p.TillDate.Date <= request.TillDate));
            }

            return await _paginationService.MapAndPaginateModelAsync<Plan, PlanDto>(plans, request);
        }
    }

}

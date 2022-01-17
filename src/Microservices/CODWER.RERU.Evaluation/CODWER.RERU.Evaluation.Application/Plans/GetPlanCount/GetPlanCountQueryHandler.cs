using CODWER.RERU.Evaluation.Application.Plans.GetCountedPlans;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlanCount
{
    public class GetPlanCountQueryHandler : IRequestHandler<GetPlanCountQuery, List<PlanCount>>
    {

        private readonly AppDbContext _appDbContext;

        public GetPlanCountQueryHandler (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<PlanCount>> Handle(GetPlanCountQuery request, CancellationToken cancellationToken)
        {

            var plans =  _appDbContext.Plans.Where(p => p.FromDate.Date >= request.FromDate.Date && p.TillDate.Date <= request.TillDate ||
                                                    (request.FromDate.Date <= p.FromDate.Date && p.FromDate.Date <= request.TillDate.Date) && (request.FromDate.Date <= p.TillDate.Date && p.TillDate.Date >= request.TillDate.Date) ||
                                                    (request.FromDate.Date >= p.FromDate.Date && p.FromDate.Date <= request.TillDate.Date) && ( request.FromDate.Date <= p.TillDate.Date && p.TillDate.Date <= request.TillDate.Date)).AsQueryable();

            var dates = new List<PlanCount>();
            
            for (var dt = request.FromDate.Date; dt <= request.TillDate.Date; dt = dt.AddDays(1))
            {
                var count = plans.Where(p => p.FromDate.Date <= dt.Date && dt.Date <= p.TillDate.Date).Count();

                var planCount = new PlanCount()
                {
                    Date = dt,
                    Count = count
                };

                dates.Add(planCount);

            }

            return dates;
            
        }

    }
}

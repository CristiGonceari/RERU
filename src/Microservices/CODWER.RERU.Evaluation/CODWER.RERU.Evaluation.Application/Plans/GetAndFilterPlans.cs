using System;
using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Plans
{
    public static class GetAndFilterPlans
    {
        public static IQueryable<Plan> Filter(AppDbContext appDbContext, string name, DateTime? fromDate, DateTime? tillDate)
        {
            var plans = appDbContext.Plans.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                plans = plans.Where(x => x.Name.Contains(name));
            }

            if (fromDate != null && tillDate != null)
            {
                plans = plans.Where(p => p.FromDate.Date >= fromDate && p.TillDate.Date <= tillDate ||
                                         (fromDate <= p.FromDate.Date && p.FromDate.Date <= tillDate) && (fromDate <= p.TillDate.Date && p.TillDate.Date >= tillDate) ||
                                         (fromDate >= p.FromDate.Date && p.FromDate.Date <= tillDate) && (fromDate <= p.TillDate.Date && p.TillDate.Date <= tillDate));
            }

            return plans;
        }
    }
}

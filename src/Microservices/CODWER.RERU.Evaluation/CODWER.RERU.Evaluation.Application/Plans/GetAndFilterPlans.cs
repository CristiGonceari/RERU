using System;
using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Plans
{
    public static class GetAndFilterPlans
    {
        public static IQueryable<Plan> Filter(AppDbContext appDbContext, string name, DateTime? fromDate, DateTime? tillDate)
        {
            var plans = appDbContext.Plans.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                plans = plans.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (fromDate != null && tillDate != null)
            {
                plans = plans.Where(x => x.FromDate >= fromDate && x.TillDate <= tillDate);
            }
            else if (fromDate != null)
            {
                plans = plans.Where(x => x.FromDate >= fromDate);
            }
            else if (tillDate != null)
            {
                plans = plans.Where(x => x.TillDate <= tillDate);
            }

            return plans;
        }
    }
}

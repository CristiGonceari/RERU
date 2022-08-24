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
                var startDate = (DateTime)fromDate;
                var endDate = (DateTime)tillDate;

                foreach (var p in plans)
                {
                    bool isIncluded = false;

                    for (var dt = p.FromDate.Date; dt <= p.TillDate; dt = dt.AddDays(1))
                    {
                        if (startDate.Date <= dt && endDate.Date >= dt)
                        {
                            isIncluded = true;
                            break;
                        }
                    }

                    if (!isIncluded)
                    {
                        plans = plans.Where(x => x.Id != p.Id);
                    }
                }
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

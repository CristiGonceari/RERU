using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events
{
    public static class GetAndFilterEvents
    {
        public static IQueryable<Event> Filter(AppDbContext appDbContext, string name, string location, DateTime? fromDate, DateTime? tillDate)
        {
            var events = appDbContext.Events
                .Include(x => x.EventLocations)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                events = events.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                events = events.Where(x => x.EventLocations.Any(l => l.Location.Name.Contains(location)) || x.EventLocations.Any(l => l.Location.Address.Contains(location)));
            }

            if (fromDate != null && tillDate != null)
            {
                var startDate = (DateTime)fromDate;
                var endDate = (DateTime)tillDate;

                foreach (var e in events) 
                {
                    bool isIncluded = false;

                    for (var dt = e.FromDate.Date; dt <= e.TillDate; dt = dt.AddDays(1))
                    {
                        if (startDate.Date <= dt && endDate.Date >= dt)
                        {
                            isIncluded = true;
                            break;
                        }
                    }

                    if (!isIncluded) 
                    {
                       events = events.Where(x => x.Id != e.Id);
                    }
                }
            }
            else if (fromDate != null)
            {
                events = events.Where(x => x.FromDate >= fromDate);
                
            }else if (tillDate != null)
            {
                events = events.Where(x => x.TillDate <= tillDate);
            }

            return events;
        }
    }
}

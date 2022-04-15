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
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                events = events.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                events = events.Where(x => x.EventLocations.Any(l => l.Location.Name.Contains(location)) || x.EventLocations.Any(l => l.Location.Address.Contains(location)));
            }

            if (fromDate != null)
            {
                events = events.Where(x => x.FromDate >= fromDate);
            }

            if (tillDate != null)
            {
                events = events.Where(x => x.TillDate <= tillDate);
            }

            return events;
        }
    }
}

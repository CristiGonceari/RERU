using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events
{
    public static class GetAndFilterEvents
    {
        public static IQueryable<Event> Filter(AppDbContext appDbContext, string name, string location)
        {
            var events = appDbContext.Events
                .Include(x => x.EventLocations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                events = events.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                events = events.Where(x => x.EventLocations.Any(l => l.Location.Name.Contains(location)) || x.EventLocations.Any(l => l.Location.Address.Contains(location)));
            }

            return events;
        }
    }
}

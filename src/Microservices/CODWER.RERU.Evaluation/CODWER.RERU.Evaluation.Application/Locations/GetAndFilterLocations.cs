using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations
{
    public static class GetAndFilterLocations
    {
        public static IQueryable<Location> Filter(AppDbContext appDbContext, string name, string address)
        {
            var locations = appDbContext.Locations
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                locations = locations.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                locations = locations.Where(x => x.Address.ToLower().Contains(address.ToLower()));
            }

            return locations;
        }
    }
}

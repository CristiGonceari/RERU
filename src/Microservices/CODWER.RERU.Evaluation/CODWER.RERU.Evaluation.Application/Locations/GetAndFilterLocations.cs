using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations
{
    public static class GetAndFilterLocations
    {
        public static IQueryable<Location> Filter(AppDbContext appDbContext, string name, string address)
        {
            var locations = appDbContext.Locations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                locations = locations.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                locations = locations.Where(x => x.Address.Contains(address));
            }

            return locations;
        }
    }
}

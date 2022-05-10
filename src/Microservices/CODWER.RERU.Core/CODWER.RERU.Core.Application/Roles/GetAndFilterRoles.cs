using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles
{
    public static class GetAndFilterRoles
    {
        public static IQueryable<Role> Filter(AppDbContext appDbContext, string name)
        {
            var roles = appDbContext.Roles
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                roles = roles.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return roles;
        }
    }
}

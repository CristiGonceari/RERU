using System.Linq;
using RERU.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Data.Persistence.Helpers
{
    public static class UserProfileFilterHelpers
    {
        public static IQueryable<UserProfile> IncludeBasic(this IQueryable<UserProfile> userProfiles)
        {
            return userProfiles
                .Include(up=>up.Identities)
                .Include(up => up.ModuleRoles.OrderByDescending(mr => mr.ModuleRole.Module.Priority))
                .ThenInclude(upmr => upmr.ModuleRole)
                .ThenInclude(mr => mr.Module)

            .Include(up => up.ModuleRoles)
            .ThenInclude(upmr => upmr.ModuleRole)
            .ThenInclude(mr => mr.Permissions)
            .ThenInclude(mrp => mrp.Permission);
        }
    }
}
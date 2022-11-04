using System.Linq;
using RERU.Data.Entities;

namespace RERU.Data.Persistence.Extensions
{
    public static class UserProfileOrdering
    {
        public static IQueryable<UserProfile> OrderByFullName(this IQueryable<UserProfile> userProfile)
        {
            return userProfile.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ThenBy(x => x.FatherName);
        }
    }
}

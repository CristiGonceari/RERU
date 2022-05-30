using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles
{
    public static class GetAndFilterUserProfiles
    {
        public static IQueryable<UserProfile> Filter(AppDbContext appDbContext, FilterUserProfilesDto request)
        {
            var userProfiles = appDbContext.UserProfiles
                .Include(x => x.Department)
                .Include(x => x.Role)
                .AsQueryable();


            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var toSearch = request.Keyword.Split(' ').ToList();

                foreach (var s in toSearch)
                {
                    userProfiles = userProfiles.Where(p =>
                        p.FirstName.ToLower().Contains(s.ToLower())
                        || p.LastName.ToLower().Contains(s.ToLower())
                        || p.FatherName.ToLower().Contains(s.ToLower()));
                }
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var toSearch = request.Email.Split(' ').ToList();

                userProfiles = userProfiles.Where(p => p.Email.ToLower().Contains(toSearch.First().ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Idnp))
            {
                var toSearch = request.Idnp.Split(' ').ToList();

                userProfiles = userProfiles.Where(p => p.Idnp.ToLower().Contains(toSearch.First().ToLower()));
            }

            if (request.Status.HasValue)
            {
                userProfiles = userProfiles.Where(p => p.IsActive == request.Status.Value);
            }

            return userProfiles;
        }
    }
}

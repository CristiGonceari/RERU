using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using CVU.ERP.Common.DataTransferObjects.Users;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.UserProfiles
{
    public static class GetAndFilterUserProfiles
    {
        public static IQueryable<UserProfile> Filter(AppDbContext appDbContext, FilterUserProfilesDto request, UserProfileDto currentUser)
        {
            var userProfiles = appDbContext.UserProfiles
                .Include(x => x.Department)
                .Include(x => x.Role)
                .OrderBy(up => up.LastName)
                .ThenBy(up => up.FirstName)
                .AsQueryable();

            if (currentUser.AccessModeEnum == AccessModeEnum.CurrentDepartment || currentUser.AccessModeEnum == null)
            {
                userProfiles = userProfiles.Where(x => x.DepartmentColaboratorId == currentUser.DepartmentColaboratorId);
            }
            else if (currentUser.AccessModeEnum == AccessModeEnum.OnlyCandidates)
            {
                userProfiles = userProfiles.Where(x => x.DepartmentColaboratorId == null && x.RoleColaboratorId == null);
            }
            else if (currentUser.AccessModeEnum == AccessModeEnum.AllDepartments)
            {
                userProfiles = userProfiles.Where(x => x.DepartmentColaboratorId != null);
            }

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

            if (request.UserStatusEnum.HasValue)
            {
                if (request.UserStatusEnum == UserStatusEnum.Employee)
                {
                    userProfiles = userProfiles.Where(x => x.DepartmentColaboratorId != null && x.RoleColaboratorId != null);
                }
                else if (request.UserStatusEnum == UserStatusEnum.Candidate)
                {
                    userProfiles = userProfiles.Where(x => x.DepartmentColaboratorId == null || x.RoleColaboratorId == null);
                }
            }

            return userProfiles;
        }
    }
}

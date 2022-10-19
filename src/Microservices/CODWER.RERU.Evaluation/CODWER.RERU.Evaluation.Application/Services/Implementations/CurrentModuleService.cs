using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class CurrentModuleService : ICurrentModuleService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public CurrentModuleService(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<UserProfileModuleRole> GetUserCurrentModuleRole()
        {
            var currentUser = await _userProfileService.GetCurrentUser();

            var currentModuleId = _appDbContext.ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Permission.Code.StartsWith("P03")).Role.ModuleId;

            var currentUserProfile = _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUser.Id);

            return currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);
        }

        public async Task<UserProfile> GetCurrentUserProfile()
        {
            var currentUser = await _userProfileService.GetCurrentUser();

            return _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUser.Id);
        }
    }
}

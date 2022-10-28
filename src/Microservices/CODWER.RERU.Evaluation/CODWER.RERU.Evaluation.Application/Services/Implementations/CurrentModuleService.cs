using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;
using RERU.Data.Persistence.ModulePrefixes;

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
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var currentModuleId = _appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);

            var currentUserProfile = _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);

            return currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);
        }

        public async Task<UserProfile> GetCurrentUserProfile()
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            return _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);
        }
    }
}

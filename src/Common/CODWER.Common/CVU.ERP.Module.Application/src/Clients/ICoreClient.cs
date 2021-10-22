using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;

namespace CVU.ERP.Module.Application.Clients
{
    public interface ICoreClient
    {
        Task<ApplicationUser> GetApplicationUserByIdentity(string id, string identityProvider);
        Task<ApplicationUser> GetApplicationUserByIdentity(string id);
        Task<ApplicationUser> GetApplicationUser(string coreUserProfileId);
        Task<bool> ExistUserInCore(string coreUserProfileId);

        Task<ApplicationUser> CreateUserProfile(InternalUserProfileCreate userProfileDto);
        Task ResetPassword(string coreUserProfileId);
        Task DeactivateUserProfile(string coreUserProfileId);
        Task<List<ModuleRolesDto>> GetModuleRoles();
    }
}
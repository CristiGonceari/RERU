using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Models;

namespace CVU.ERP.ServiceProvider.Clients
{
    public interface ICoreClient
    {
        Task<ApplicationUser> GetApplicationUserByIdentity(string id, string identityProvider);
        Task<ApplicationUser> GetApplicationUserByIdentity(string id);
        Task<ApplicationUser> GetApplicationUser(string coreUserProfileId);
        //Task<bool> ExistUserInCore(string coreUserProfileId);

        //Task<ApplicationUser> CreateUserProfile(InternalUserProfileCreate userProfileDto);
        //Task ResetPassword(string coreUserProfileId);
        //Task DeactivateUserProfile(string coreUserProfileId);
        //Task<List<ModuleRolesDto>> GetModuleRoles();
    }
}
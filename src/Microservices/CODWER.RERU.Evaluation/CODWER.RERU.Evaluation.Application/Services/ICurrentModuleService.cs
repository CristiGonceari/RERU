using RERU.Data.Entities;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface ICurrentModuleService
    {
        Task<UserProfileModuleRole> GetUserCurrentModuleRole();
        Task<UserProfile> GetCurrentUserProfile();
    }
}

using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IUserProfileService
    {
        Task<int> GetCurrentUserId();
        Task<UserProfileDto> GetCurrentUserProfileDto();
        Task<UserProfile> GetCurrentUserProfile();
    }
}

using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetCurrentUser();
    }
}

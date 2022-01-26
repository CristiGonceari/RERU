using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.User;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IUserProfileService
    {
        public Task<UserProfile> GetCurrentUserProfile();
        public Task<int> GetCurrentContractorId();
        public Task<Contractor> GetCurrentContractor();
    }
}

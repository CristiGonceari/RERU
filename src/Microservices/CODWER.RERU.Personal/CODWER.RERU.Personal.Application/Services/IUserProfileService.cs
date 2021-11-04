using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.User;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IUserProfileService
    {
        public Task<UserProfile> GetCurrentUserProfile();
        public Task<int> GetCurrentContractorId();
        public Task<Contractor> GetCurrentContractor();
    }
}

using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IUserProfileService
    {
        public Task<UserProfile> GetCurrentUserProfile();
        public Task<int> GetCurrentContractorId();
        public Task<Contractor> GetCurrentContractor();
    }
}

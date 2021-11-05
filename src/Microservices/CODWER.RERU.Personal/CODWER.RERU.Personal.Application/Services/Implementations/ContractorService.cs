using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class ContractorService : IContractorService
    {
        private readonly IUserProfileService _userProfileService;
        private readonly AppDbContext _appDbContext;

        public ContractorService(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            _userProfileService = userProfileService;
            _appDbContext = appDbContext;
        }

        public async Task<Contractor> GetCurrentContractor()
        {
            var currentUserProfile = await _userProfileService.GetCurrentUserProfile();

            if (currentUserProfile == null)
            {
                return null;
            }

            var currentContractor = await _appDbContext.Contractors
                .FirstOrDefaultAsync(x => x.Id == currentUserProfile.ContractorId);

            return currentContractor;
        }
    }

}

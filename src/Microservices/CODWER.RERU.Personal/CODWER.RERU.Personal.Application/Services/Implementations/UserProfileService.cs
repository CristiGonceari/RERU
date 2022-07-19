using System;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Providers;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ICurrentApplicationUserProvider _currentApplicationUserProvider;
        private readonly AppDbContext _appDbContext;

        public UserProfileService(ICurrentApplicationUserProvider currentApplicationUserProvider, AppDbContext appDbContext)
        {
            _currentApplicationUserProvider = currentApplicationUserProvider;
            _appDbContext = appDbContext;
        }

        public async Task<UserProfile> GetCurrentUserProfile()
        {
            var user = await _currentApplicationUserProvider.Get();

            if (user == null)
            {
                Console.WriteLine("----GetUserProfile - null User in session");
                return null;
            }

            var userProfile = await _appDbContext.UserProfiles
                .Include(x => x.Contractor)
                .FirstOrDefaultAsync(x => x.Id == int.Parse(user.Id));

            return userProfile ?? new UserProfile();
        }

        public async Task<int> GetCurrentContractorId()
        {
            var userProfile = await GetCurrentUserProfile();

            if (userProfile.Contractor == null)
            {
                throw new Exception(ValidationCodes.NONEXISTENT_USER_PROFILE_CONTRACTOR);
            }

            return (int) userProfile.Contractor.Id;
        }

        public async Task<Contractor> GetCurrentContractor()
        {
            var contractorId = await GetCurrentContractorId();

            var contractor = await _appDbContext.Contractors
                .Include(x=>x.UserProfile)
                .FirstOrDefaultAsync(x => x.Id == contractorId);

            return contractor;
        }
    }
}

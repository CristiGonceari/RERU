using System;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.User;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Providers;
using Microsoft.EntityFrameworkCore;

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
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (userProfile == null)
            {
                return await CreateUserProfile(user);
            }

            return userProfile;
        }

        public async Task<int> GetCurrentContractorId()
        {
            var userProfile = await GetCurrentUserProfile();

            if (userProfile.ContractorId == null)
            {
                throw new Exception(ValidationCodes.NONEXISTENT_USER_PROFILE_CONTRACTOR);
            }

            return (int) userProfile.ContractorId;
        }

        public async Task<Contractor> GetCurrentContractor()
        {
            var contractorId = await GetCurrentContractorId();

            var contractor = await _appDbContext.Contractors
                .FirstOrDefaultAsync(x => x.Id == contractorId);

            return contractor;
        }

        private async Task<UserProfile> CreateUserProfile(ApplicationUser appUser)
        {
            var checkExistent = await _appDbContext.UserProfiles.AnyAsync(x => x.Email == appUser.Email);
            
            if (checkExistent)
            {
                throw new Exception("User profile with this email exist");
            }

            var userProfile = new UserProfile
            {
                ContractorId = null,
                UserId = appUser.Id,
                Email = appUser.Email
            };

            await _appDbContext.UserProfiles.AddAsync(userProfile);
            await _appDbContext.SaveChangesAsync();

            return userProfile;
        }
    }
}

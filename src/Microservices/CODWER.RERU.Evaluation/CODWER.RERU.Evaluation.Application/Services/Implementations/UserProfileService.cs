using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Providers;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IEnumerable<ICurrentApplicationUserProvider> _userProvider;
        public UserProfileService(AppDbContext appDbContext, IMapper mapper, IEnumerable<ICurrentApplicationUserProvider> userProvider)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<UserProfileDto> GetCurrentUser()
        {
            var coreUser = await _userProvider.FirstOrDefault(x => x.IsAuthenticated)?.Get();
            var currentUserProfile = new UserProfile();

            if (coreUser != null)
            {
                currentUserProfile = _appDbContext.UserProfiles
                    .FirstOrDefault(x => x.CoreUserId == coreUser.Id);


                if (currentUserProfile == null)
                {
                    currentUserProfile = _mapper.Map<UserProfile>(coreUser);
                    await _appDbContext.UserProfiles.AddAsync(currentUserProfile);
                    await _appDbContext.SaveChangesAsync();
                }
            }

            var userToReturn = _mapper.Map<UserProfileDto>(currentUserProfile);
            userToReturn.Permissions = coreUser?.Permissions;
            return userToReturn;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.ServiceProvider;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
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

        public async Task<UserProfileDto> GetCurrentUserProfileDto()
        {
            var coreUser = await _userProvider.FirstOrDefault(x => x.IsAuthenticated)?.Get();
            var currentUserProfile = new UserProfile
            {
                Contractor = new Contractor()
            };

            if (coreUser != null)
            {
                currentUserProfile = _appDbContext.UserProfiles
                    .Include(x => x.Department)
                    .Include(x => x.Role)
                    .Include(x => x.Contractor)
                    .Select(x => new UserProfile
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        FatherName = x.FatherName,
                        Idnp = x.Idnp,
                        Email = x.Email,
                        MediaFileId = x.MediaFileId,
                        RequiresDataEntry = x.RequiresDataEntry,
                        PhoneNumber = x.PhoneNumber,
                        BirthDate = x.BirthDate,
                        Token = x.Token,
                        IsActive = x.IsActive,
                        TokenLifetime = x.TokenLifetime,
                        AccessModeEnum = x.AccessModeEnum,
                        Password = x.Password,
                        RoleColaboratorId = x.RoleColaboratorId,
                        DepartmentColaboratorId = x.DepartmentColaboratorId,
                        FunctionColaboratorId = x.FunctionColaboratorId
                    })
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == int.Parse(coreUser.Id));


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

        public async Task<UserProfile> GetCurrentUserProfile()
        {
            var coreUser = await _userProvider.FirstOrDefault(x => x.IsAuthenticated)?.Get();
            var currentUserProfile = new UserProfile
            {
                Contractor = new Contractor()
            };

            if (coreUser != null)
            {
                currentUserProfile = _appDbContext.UserProfiles
                    .Include(x => x.Department)
                    .Include(x => x.Role)
                    .Include(x => x.Contractor)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == int.Parse(coreUser.Id));

                if (currentUserProfile == null)
                {
                    currentUserProfile = _mapper.Map<UserProfile>(coreUser);
                    await _appDbContext.UserProfiles.AddAsync(currentUserProfile);
                    await _appDbContext.SaveChangesAsync();
                }
            }

            return currentUserProfile;
        }

        public async Task<int> GetCurrentUserId()
        {
            var coreUser = await _userProvider.FirstOrDefault(x => x.IsAuthenticated)?.Get();

            return int.Parse(coreUser.Id);
        }
    }
}

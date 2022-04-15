using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Providers;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Module.Providers
{
    public class CoreApplicationUserProvider : IApplicationUserProvider
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        private const string DEFAULT_IDENTITY_SERVICE = "local";

        public CoreApplicationUserProvider(ICommonServiceProvider commonServiceProvider)
        {
            _appDbContext = commonServiceProvider.AppDbContext;
            _mapper = commonServiceProvider.Mapper;
        }

        public async Task<ApplicationUser> Get(string id, string identityProvider = null)
        {
            identityProvider = identityProvider ?? DEFAULT_IDENTITY_SERVICE;

            var userProfile = await _appDbContext.UserProfiles
                                         .IncludeBasic()
                                         .FirstOrDefaultAsync(up => up.Identities.Any(upi => upi.Identificator == id && upi.Type == identityProvider));
            //if (userProfile == null)
            //{
            //    userProfile = new Data.Entities.UserProfile();

            //    userProfile.IsActive = true;
            //    userProfile.RequiresDataEntry = true;
            //    userProfile.Identities.Add(new Data.Entities.UserProfileIdentity { Identificator = id, Type = identityProvider });
                
            //    _coreDbContext.UserProfiles.Add(userProfile);
            //    await _coreDbContext.SaveChangesAsync();
            //}

            if (userProfile.IsActive == false)
            {
                throw new Exception(userProfile.FirstName + ' ' + userProfile.LastName + " was deactivated");
            }

            return _mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
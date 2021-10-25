using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.CreateInternalUserProfile
{
    public class CreateInternalUserProfileCommandHandler : BaseHandler, IRequestHandler<CreateInternalUserProfileCommand, ApplicationUser>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;

        public CreateInternalUserProfileCommandHandler(ICommonServiceProvider commonServiceProvider, IEnumerable<IIdentityService> identityServices) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
        }

        public async Task<ApplicationUser> Handle(CreateInternalUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = Mapper.Map<UserProfile>(request.Data);


            if (request.Data.ModuleRoles != null)
            {
                foreach (var moduleRole in request.Data.ModuleRoles)
                {
                    userProfile.ModuleRoles.Add(new UserProfileModuleRole
                    {
                        ModuleRoleId = moduleRole,
                    });
                }
            }
            else
            {
                var defaultRoles = CoreDbContext.Modules
                    .SelectMany(m => m.Roles.Where(r => r.IsAssignByDefault).Take(1))
                    .ToList();

                foreach (var role in defaultRoles)
                {
                    userProfile.ModuleRoles.Add(new UserProfileModuleRole
                    {
                        ModuleRole = role
                    });
                }
            }

            foreach (var identityService in _identityServices)
            {
                var identifier = await identityService.Create(userProfile, request.Data.NotifyAccountCreated);

                if (!string.IsNullOrEmpty(identifier))
                {
                    userProfile.Identities.Add(new UserProfileIdentity
                    {
                        Identificator = identifier,
                        Type = identityService.Type
                    });
                }
            }

            CoreDbContext.UserProfiles.Add(userProfile);
            await CoreDbContext.SaveChangesAsync();

            userProfile = await CoreDbContext.UserProfiles
                .IncludeBasic()
                .FirstOrDefaultAsync(up => up.Id == userProfile.Id);

            return Mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.CreateInternalUserProfile
{
    public class CreateInternalUserProfileCommandHandler : BaseHandler, IRequestHandler<CreateInternalUserProfileCommand, ApplicationUser>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly IEvaluationClient _evaluationClient;

        public CreateInternalUserProfileCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            IEvaluationClient evaluationClient
            ) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _evaluationClient = evaluationClient;
        }

        public async Task<ApplicationUser> Handle(CreateInternalUserProfileCommand request, CancellationToken cancellationToken)
        {
            var existUserProfileByIdnp = await AppDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Idnp == request.Data.Idnp);

            if (existUserProfileByIdnp == null)
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
                    var defaultRoles = AppDbContext.Modules
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

                AppDbContext.UserProfiles.Add(userProfile);
                await AppDbContext.SaveChangesAsync();

                return await GetApplicationUserAndSync(userProfile.Id);
            }

            Mapper.Map(request.Data, existUserProfileByIdnp);

            return await GetApplicationUserAndSync(existUserProfileByIdnp.Id);
        }

        private async Task<ApplicationUser> GetApplicationUserAndSync(int userProfileId)
        {
            var userProfile = await AppDbContext.UserProfiles
                .IncludeBasic()
                .FirstOrDefaultAsync(up => up.Id == userProfileId);

            return Mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
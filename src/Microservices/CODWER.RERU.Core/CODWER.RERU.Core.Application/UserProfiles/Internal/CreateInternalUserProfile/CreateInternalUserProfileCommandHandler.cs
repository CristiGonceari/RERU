using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Services;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.CreateInternalUserProfile
{
    public class CreateInternalUserProfileCommandHandler : BaseHandler, IRequestHandler<CreateInternalUserProfileCommand, ApplicationUser>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly IEvaluationUserProfileService _evaluationUserProfileService;
        private readonly IMapper _mapper;

        public CreateInternalUserProfileCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            IEvaluationUserProfileService evaluationUserProfileService,
            IMapper mapper
            ) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _evaluationUserProfileService = evaluationUserProfileService;
            _mapper = mapper;
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

            await _evaluationUserProfileService.Sync(_mapper.Map<BaseUserProfile>(userProfile));

            return Mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
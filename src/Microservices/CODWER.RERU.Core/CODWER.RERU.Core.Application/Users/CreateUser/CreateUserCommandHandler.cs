using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;
using MediatR;
using System.Text.Json;
using AutoMapper;
using CODWER.RERU.Core.Application.Services;
using CVU.ERP.Module.Application.Models.Internal;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<CreateUserCommandHandler> _loggerService;
        private readonly IEvaluationUserProfileService _evaluationUserProfileService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices, 
            ILoggerService<CreateUserCommandHandler> loggerService, IEvaluationUserProfileService evaluationUserProfileService, IMapper mapper)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _evaluationUserProfileService = evaluationUserProfileService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userProfile = Mapper.Map<UserProfile>(request.User);
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

            foreach (var identityService in _identityServices)
            {
                var identifier = await identityService.Create(userProfile, request.User.EmailNotification);

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

            await LogAction(userProfile);
            await SyncUserProfile(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($"User {userProfile.Name} {userProfile.LastName} was added to system", userProfile));
        }

        private async Task SyncUserProfile(UserProfile userProfile)
        {
            await _evaluationUserProfileService.Sync(_mapper.Map<BaseUserProfile>(userProfile));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Data.Entities;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;

        public CreateUserCommandHandler(ICommonServiceProvider commonServiceProvider, IEnumerable<IIdentityService> identityServices)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
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

            return userProfile.Id;
        }
    }
}
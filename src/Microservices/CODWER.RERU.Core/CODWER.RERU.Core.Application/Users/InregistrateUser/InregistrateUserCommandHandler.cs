using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.InregistrateUser
{
    public class InregistrateUserCommandHandler : BaseHandler, IRequestHandler<InregistrateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<InregistrateUserCommandHandler> _loggerService;
        private readonly IPasswordGenerator _passwordGenerator;

        public InregistrateUserCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<InregistrateUserCommandHandler> loggerService, IPasswordGenerator passwordGenerator)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _passwordGenerator = passwordGenerator;
        }

        public async Task<int> Handle(InregistrateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new CreateUserDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FatherName = request.FatherName,
                Idnp = request.Idnp,
                Email = request.Email,
                CandidatePositionId = request.CandidatePositionId,
                EmailNotification = request.EmailNotification,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber,
            };

            var userProfile = Mapper.Map<UserProfile>(newUser);
            userProfile.Contractor = new Contractor() { UserProfile = userProfile };

            var defaultRoles = AppDbContext.Modules
                .SelectMany(m => m.Roles.Where(r => r.IsAssignByDefault).Take(1))
                .ToList();
            
            var password = _passwordGenerator.Generate();

            foreach (var role in defaultRoles)
            {
                userProfile.ModuleRoles.Add(new UserProfileModuleRole
                {
                    ModuleRole = role
                });
            }

            foreach (var identityService in _identityServices)
            {
                var identifier = await identityService.Create(userProfile, true, password);

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

            await LogAction(userProfile);

            //await SyncUserProfile(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.LogWithoutUser(LogData.AsCore($@"Utilizatorul ""{userProfile.FullName}"" s-a înregistrat în sistem", userProfile));
        }
    }
}

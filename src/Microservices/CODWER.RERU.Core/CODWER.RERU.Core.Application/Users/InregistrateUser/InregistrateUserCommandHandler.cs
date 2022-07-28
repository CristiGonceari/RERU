using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Clients;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Users.InregistrateUser
{
    public class InregistrateUserCommandHandler : BaseHandler, IRequestHandler<InregistrateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<InregistrateUserCommandHandler> _loggerService;
        private readonly IEvaluationClient _evaluationClient;
        private readonly IMapper _mapper;

        public InregistrateUserCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<InregistrateUserCommandHandler> loggerService,
            IEvaluationClient evaluationClient,
            IMapper mapper)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _evaluationClient = evaluationClient;
            _mapper = mapper;
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
                PhoneNumber = request.PhoneNumber
            };

            var userProfile = Mapper.Map<UserProfile>(newUser);
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

            foreach (var identityService in _identityServices)
            {
                var identifier = await identityService.Create(userProfile, request.EmailNotification);

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
            await _loggerService.LogWithoutUser(LogData.AsCore($"User {userProfile.FirstName} {userProfile.LastName} was inregistrated to system", userProfile));
        }
    }
}

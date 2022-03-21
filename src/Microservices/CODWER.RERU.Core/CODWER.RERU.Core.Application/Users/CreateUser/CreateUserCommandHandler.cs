using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Logging.Models;
using MediatR;
using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.Models.Internal;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.StorageService;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<CreateUserCommandHandler> _loggerService;
        private readonly IEvaluationClient _evaluationClient;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices, 
            ILoggerService<CreateUserCommandHandler> loggerService, 
            IEvaluationClient evaluationClient, 
            IMapper mapper, 
            IStorageFileService storageFileService)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _evaluationClient = evaluationClient;
            _storageFileService = storageFileService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var storage = await _storageFileService.AddFile(request.FileDto);

            var newUser = new CreateUserDto()
            {
                Name = request.Name,
                LastName = request.LastName,
                FatherName = request.FatherName,
                Idnp = request.Idnp,
                MediaFileId = storage,
                Email = request.Email,
                EmailNotification = request.EmailNotification,
            };

            var userProfile = Mapper.Map<UserProfile>(newUser);
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
            await _evaluationClient.SyncUserProfile(_mapper.Map<BaseUserProfile>(userProfile));
        }
    }
}
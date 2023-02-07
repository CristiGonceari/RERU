using AutoMapper;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<CreateUserCommandHandler> _loggerService;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordGenerator _passwordGenerator;

        public CreateUserCommandHandler(
            IEnumerable<IIdentityService> identityServices, 
            ILoggerService<CreateUserCommandHandler> loggerService, 
            IMapper mapper,
            AppDbContext appDbContext, 
            IPasswordGenerator passwordGenerator)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _mapper = mapper;
            _passwordGenerator = passwordGenerator;
            _appDbContext = appDbContext.NewInstance();
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new CreateUserDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FatherName = request.FatherName,
                Idnp = request.Idnp,
                Email = request.Email,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber,
                DepartmentColaboratorId = request.DepartmentColaboratorId == 0 ? null : request.DepartmentColaboratorId,
                RoleColaboratorId = request.RoleColaboratorId == 0 ? null : request.RoleColaboratorId,
                FunctionColaboratorId = request.FunctionColaboratorId == 0 ? null : request.FunctionColaboratorId,
                EmailNotification = request.EmailNotification,
                AccessModeEnum = (int)request.AccessModeEnum
            };

            var userProfile = _mapper.Map<UserProfile>(newUser);
            userProfile.Contractor = new Contractor() { UserProfile = userProfile };
            
            var defaultRoles = _appDbContext.Modules
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

            userProfile.Password = password;

            _appDbContext.UserProfiles.Add(userProfile);
            await _appDbContext.SaveChangesAsync();

            await LogAction(userProfile);

            //await SyncUserProfile(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($@"Utilizatorul ""{userProfile.FullName}"" a fost adăugat în sistem", userProfile));
        }

        //private async Task SyncUserProfile(UserProfile userProfile)
        //{
        //    await _evaluationClient.SyncUserProfile(_mapper.Map<BaseUserProfile>(userProfile));
        //}
    }
}
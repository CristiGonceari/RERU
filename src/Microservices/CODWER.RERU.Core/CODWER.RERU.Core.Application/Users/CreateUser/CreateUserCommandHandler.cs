using System;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<CreateUserCommandHandler> _loggerService;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IEnumerable<IIdentityService> identityServices, 
            ILoggerService<CreateUserCommandHandler> loggerService, 
            IMapper mapper,
            IConfiguration configuration)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _mapper = mapper;
            _appDbContext = AppDbContext.NewInstance(configuration);
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
                EmailNotification = request.EmailNotification,
                AccessModeEnum = (int)request.AccessModeEnum
            };

            var userProfile = _mapper.Map<UserProfile>(newUser);
            
            var defaultRoles = _appDbContext.Modules
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

            _appDbContext.UserProfiles.Add(userProfile);
            await _appDbContext.SaveChangesAsync();

            await LogAction(userProfile);

            //await SyncUserProfile(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($"User {userProfile.FirstName} {userProfile.LastName} was added to system", userProfile));
        }

        //private async Task SyncUserProfile(UserProfile userProfile)
        //{
        //    await _evaluationClient.SyncUserProfile(_mapper.Map<BaseUserProfile>(userProfile));
        //}
    }
}
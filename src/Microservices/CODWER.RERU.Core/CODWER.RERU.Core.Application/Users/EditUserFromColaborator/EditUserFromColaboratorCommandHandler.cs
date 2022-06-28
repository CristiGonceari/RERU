using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Logging;
using CVU.ERP.Module.Application.Clients;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditUserFromColaborator
{
    public class EditUserFromColaboratorCommandHandler : BaseHandler, IRequestHandler<EditUserFromColaboratorCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<EditUserFromColaboratorCommandHandler> _loggerService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public EditUserFromColaboratorCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<EditUserFromColaboratorCommandHandler> loggerService,
            IMapper mapper, AppDbContext appDbContext,
            IServiceProvider serviceProvider)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _mapper = mapper;
            _appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        }

        public async Task<int> Handle(EditUserFromColaboratorCommand request, CancellationToken cancellationToken)
        {
            var editUser = new EditUserFromColaboratorDto()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FatherName = request.FatherName,
                Idnp = request.Idnp,
                Birthday = request.Birthday,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                DepartmentColaboratorId = request.DepartmentColaboratorId == 0 ? null : request.DepartmentColaboratorId,
                RoleColaboratorId = request.RoleColaboratorId == 0 ? null : request.RoleColaboratorId,
                EmailNotification = request.EmailNotification,
                AccessModeEnum = request.AccessModeEnum
            };

            var user = _appDbContext.UserProfiles.FirstOrDefault(x => x.Idnp == editUser.Idnp);

            if (editUser.Email != user.Email)
            {
                foreach (var identityService in _identityServices)
                {
                    var userName = $"{request.FirstName} {request.LastName}";

                    var identifier = await identityService.Update(userName, request.Email, user.Email, request.EmailNotification);

                    if (!string.IsNullOrEmpty(identifier))
                    {
                        user.Identities.Add(new UserProfileIdentity
                        {
                            Identificator = identifier,
                            Type = identityService.Type
                        });
                    }
                }
            }

            _mapper.Map(editUser, user);
            await _appDbContext.SaveChangesAsync();

            return editUser.Id;
        }
    }
}

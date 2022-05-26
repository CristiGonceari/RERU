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
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditUserFromColaborator
{
    public class EditUserFromColaboratorCommandHandler : BaseHandler, IRequestHandler<EditUserFromColaboratorCommand, int>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<EditUserFromColaboratorCommandHandler> _loggerService;
        private readonly IEvaluationClient _evaluationClient;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public EditUserFromColaboratorCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<EditUserFromColaboratorCommandHandler> loggerService,
            IEvaluationClient evaluationClient,
            IMapper mapper, AppDbContext appDbContext)
            : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
            _evaluationClient = evaluationClient;
            _mapper = mapper;
            _appDbContext = appDbContext;
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
                Email = request.Email,
                DepartmentColaboratorId = request.DepartmentColaboratorId == 0 ? null : request.DepartmentColaboratorId,
                RoleColaboratorId = request.RoleColaboratorId == 0 ? null : request.RoleColaboratorId,
                EmailNotification = request.EmailNotification
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

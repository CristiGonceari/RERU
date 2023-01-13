using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails
{
    public class EditUserPersonalDetailsCommandHandler : BaseHandler, IRequestHandler<EditUserPersonalDetailsCommand, int>
    {
        private readonly ILoggerService<EditUserPersonalDetailsCommandHandler> _loggerService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IIdentityService> _identityServices;

        public EditUserPersonalDetailsCommandHandler(ICommonServiceProvider commonServiceProvider,
            ILoggerService<EditUserPersonalDetailsCommandHandler> loggerService, 
            IMapper mapper, IEnumerable<IIdentityService> identityServices) : base(commonServiceProvider)
        {
            _mapper = mapper;
            _identityServices = identityServices;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(EditUserPersonalDetailsCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.Data.Id);

            if (request.Data.Email != userProfile.Email)
            {
                foreach (var identityService in _identityServices)
                {
                    var userName = $"{request.Data.LastName} {request.Data.FirstName} {request.Data.FatherName}";

                    var identifier = await identityService.Update(userName, request.Data.Email, userProfile.Email, request.Data.EmailNotification);

                    if (!string.IsNullOrEmpty(identifier))
                    {
                        userProfile.Identities.Add(new UserProfileIdentity
                        {
                            Identificator = identifier,
                            Type = identityService.Type
                        });
                    }
                }
            }

            _mapper.Map(request.Data, userProfile);
            await AppDbContext.SaveChangesAsync();

            await LogAction(userProfile);

            return userProfile.Id;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($@"Utilizatorul ""{userProfile.FullName}"" a fost actualizat în sistem", userProfile));
        }
    }
}
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Users.ResetUserPassword
{
    public class ResetUserPasswordCommandHandler : BaseHandler, IRequestHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<ResetUserPasswordCommandHandler> _loggerService;
        private const string DEFAULT_IDENTITY_SERVICE = "local";

        public ResetUserPasswordCommandHandler(
            ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices, 
            ILoggerService<ResetUserPasswordCommandHandler> loggerService) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext
              .UserProfiles
                  .Include(up => up.Identities)
                  .FirstOrDefaultAsync(up => up.Id == request.Id);

            if (userProfile != null)
            {
                var identityService = _identityServices.FirstOrDefault(@is => @is.Type == DEFAULT_IDENTITY_SERVICE);
                if (identityService != null)
                {
                    var identity = userProfile.Identities.FirstOrDefault(upi => upi.Type == DEFAULT_IDENTITY_SERVICE);
                    if (identity != null)
                    {
                        await identityService.ResetPassword(identity.Identificator);
                    }
                }

                await LogAction(userProfile);
            }

            return Unit.Value;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($" User {userProfile.FirstName} {userProfile.LastName}, password was reset", userProfile));
        }
    }
}
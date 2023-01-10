using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Users.ResetUserPassword;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.ResetUserPasswordByEmailCode
{
    public class ResetUserPasswordByEmailCodeCommandHandler : BaseHandler, IRequestHandler<ResetUserPasswordByEmailCodeCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<ResetUserPasswordCommandHandler> _loggerService;
        private const string DEFAULT_IDENTITY_SERVICE = "local";

        public ResetUserPasswordByEmailCodeCommandHandler(
            ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<ResetUserPasswordCommandHandler> loggerService) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
        }


        public async Task<Unit> Handle(ResetUserPasswordByEmailCodeCommand request, CancellationToken cancellationToken)
        {

            //var emailVerification = AppDbContext.EmailVerifications.Where
            var userProfile = await AppDbContext
              .UserProfiles
                  .Include(up => up.Identities)
                  .FirstOrDefaultAsync(up => up.Email == request.Email);

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
            await _loggerService.Log(LogData.AsCore($@"Parola utilizatorului ""{userProfile.FullName}"" a fost resetată", userProfile));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ResetUserPassword
{
    public class ResetUserPasswordCommandHandler : BaseHandler, IRequestHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private const string DEFAULT_IDENTITY_SERVICE = "local";


        public ResetUserPasswordCommandHandler(
            ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
        }

        public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await CoreDbContext
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
            }
            return Unit.Value;
        }
    }
}
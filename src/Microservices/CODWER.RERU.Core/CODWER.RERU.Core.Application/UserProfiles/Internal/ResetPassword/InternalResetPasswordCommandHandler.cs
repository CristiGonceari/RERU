using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.ResetPassword
{
    public class InternalResetPasswordCommandHandler : BaseHandler, IRequestHandler<InternalResetPasswordCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private const string DEFAULT_IDENTITY_SERVICE = "local";

        public InternalResetPasswordCommandHandler(ICommonServiceProvider commonServiceProvider, IEnumerable<IIdentityService> identityServices) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
        }

        public async Task<Unit> Handle(InternalResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await CoreDbContext.UserProfiles
                    .Include(up => up.Identities)
                    .FirstOrDefaultAsync(up => up.Id == request.UserProfileId);

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
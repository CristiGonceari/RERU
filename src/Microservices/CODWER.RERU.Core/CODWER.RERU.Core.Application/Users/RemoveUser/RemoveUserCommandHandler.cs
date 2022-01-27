using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;

namespace CODWER.RERU.Core.Application.Users.RemoveUser
{
    public class RemoveUserCommandHandler : BaseHandler, IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;
        private readonly ILoggerService<RemoveUserCommandHandler> _loggerService;

        public RemoveUserCommandHandler(ICommonServiceProvider commonServiceProvider,
            IEnumerable<IIdentityService> identityServices,
            ILoggerService<RemoveUserCommandHandler> loggerService) : base(commonServiceProvider)
        {
            _identityServices = identityServices;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await CoreDbContext.UserProfiles
                .Include(x => x.Identities)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            foreach (var identity in userProfile.Identities)
            {
                var service = _identityServices.FirstOrDefault(s => s.Type == identity.Type);
                service.Remove(identity.Identificator);
            }

            CoreDbContext.UserProfiles.Remove(userProfile);

            await CoreDbContext.SaveChangesAsync();

            await LogAction(userProfile);

            return Unit.Value;
        }

        private async Task LogAction(UserProfile userProfile)
        {
            await _loggerService.Log(LogData.AsCore($"User {userProfile.Name} {userProfile.LastName} was removed", userProfile));
        }
    }
}

using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.RemoveUser
{
    public class RemoveUserCommandHandler : BaseHandler, IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly IEnumerable<IIdentityService> _identityServices;

        public RemoveUserCommandHandler(ICommonServiceProvider commonServicepProvider, IEnumerable<IIdentityService> identityServices) : base(commonServicepProvider)
        {
            _identityServices = identityServices;
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

            return Unit.Value;
        }
    }
}

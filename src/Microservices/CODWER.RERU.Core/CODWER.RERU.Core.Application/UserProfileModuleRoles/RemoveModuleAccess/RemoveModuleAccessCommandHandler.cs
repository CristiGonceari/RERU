using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.RemoveModuleAccess {
    public class RemoveModuleAccessCommandHandler : BaseHandler, IRequestHandler<RemoveModuleAccessCommand, Unit> {
        public RemoveModuleAccessCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<Unit> Handle (RemoveModuleAccessCommand request, CancellationToken cancellationToken) {
            var upmrToBeRemoved = await CoreDbContext.UserProfileModuleRoles.FirstOrDefaultAsync (upmr => upmr.UserProfileId == request.UserId && upmr.ModuleRole.ModuleId == request.ModuleId);
            if (upmrToBeRemoved != null) {
                CoreDbContext.UserProfileModuleRoles.Remove (upmrToBeRemoved);
                await CoreDbContext.SaveChangesAsync ();
            }
            return Unit.Value;
        }
    }
}
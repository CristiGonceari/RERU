using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.RemoveModuleAccess {
    public class RemoveModuleAccessCommandHandler : BaseHandler, IRequestHandler<RemoveModuleAccessCommand, Unit> 
    {
        public RemoveModuleAccessCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<Unit> Handle (RemoveModuleAccessCommand request, CancellationToken cancellationToken) 
        {
            var upmrToBeRemoved = await AppDbContext.UserProfileModuleRoles
                .FirstOrDefaultAsync (upmr => upmr.UserProfileId == request.UserId 
                                              && upmr.ModuleRole.ModuleId == request.ModuleId);

            if (upmrToBeRemoved != null) {
                AppDbContext.UserProfileModuleRoles.Remove (upmrToBeRemoved);
                await AppDbContext.SaveChangesAsync ();
            }

            return Unit.Value;
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfileModuleRoles.UpdateUserProfileModuleAccess {
    public class UpdateUserProfileModuleAccessCommandHandler : BaseHandler, IRequestHandler<UpdateUserProfileModuleAccessCommand, Unit> {
        public UpdateUserProfileModuleAccessCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<Unit> Handle (UpdateUserProfileModuleAccessCommand request, CancellationToken cancellationToken) 
        {
            var userProfile = await CoreDbContext.UserProfiles
                .FirstOrDefaultAsync (up => up.Id == request.Data.UserId);

            var moduleRole = await CoreDbContext.ModuleRoles
                .FirstOrDefaultAsync(mr => mr.ModuleId == request.Data.ModuleId 
                                            && mr.Id == request.Data.RoleId);

            if (moduleRole != null && userProfile != null) {
                var upmr = await CoreDbContext.UserProfileModuleRoles
                    .FirstOrDefaultAsync(upmr => upmr.ModuleRole.ModuleId == request.Data.ModuleId 
                                                  && upmr.UserProfileId == request.Data.UserId);

                if (upmr is null) {
                    upmr = new UserProfileModuleRole {
                        UserProfile = userProfile
                    };
                    
                    CoreDbContext.UserProfileModuleRoles.Add(upmr);
                }

                upmr.ModuleRole = moduleRole;
                await CoreDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
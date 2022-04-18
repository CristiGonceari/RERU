using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.DeactivateUserProfile
{
    public class InternalDeactivateUserProfileCommandHandler : BaseHandler, IRequestHandler<InternalDeactivateUserProfileCommand, Unit>
    {
        public InternalDeactivateUserProfileCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<Unit> Handle(InternalDeactivateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.UserProfileId);
            if (userProfile != null)
            {
                userProfile.IsActive = false;
                await AppDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
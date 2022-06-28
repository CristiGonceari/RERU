using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.BlockUser {
    public class BlockUnblockUserCommandHandler : BaseHandler, IRequestHandler<BlockUnblockUserCommand, Unit> 
    {
       
        public BlockUnblockUserCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<Unit> Handle (BlockUnblockUserCommand request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync (up => up.Id == request.Id);

            if (userProfile != null) 
            {
                userProfile.IsActive = false;
                await AppDbContext.SaveChangesAsync ();
            }

            return Unit.Value;
        }
    }
}
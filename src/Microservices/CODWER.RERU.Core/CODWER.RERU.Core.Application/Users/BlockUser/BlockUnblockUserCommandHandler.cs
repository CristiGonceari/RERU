using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.BlockUser {
    class BlockUnblockUserCommandHandler : BaseHandler, IRequestHandler<BlockUnblockUserCommand, Unit> {
        private readonly UserManager<ERPIdentityUser> _userManager;
        public BlockUnblockUserCommandHandler (
            ICommonServiceProvider commonServicepProvider,
            UserManager<ERPIdentityUser> userManager
        ) : base (commonServicepProvider) {
            _userManager = userManager;
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
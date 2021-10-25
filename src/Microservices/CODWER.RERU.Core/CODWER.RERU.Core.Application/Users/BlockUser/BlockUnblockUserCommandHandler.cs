using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
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

        public async Task<Unit> Handle (BlockUnblockUserCommand request, CancellationToken cancellationToken) {
            var userProfile = await CoreDbContext.UserProfiles.FirstOrDefaultAsync (up => up.Id == request.Id);
            if (userProfile != null) {
                userProfile.IsActive = false;
                await CoreDbContext.SaveChangesAsync ();
            }
            // if (userProfile != null && !string.IsNullOrEmpty(userProfile.UserId))
            // {
            //     var user = await UserManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == userProfile.UserId);
            //     if (user != null)
            //     {
            //         user.LockoutEnabled = true;
            //         if (user.LockoutEnd < DateTime.MaxValue)
            //         {
            //             userProfile.IsActive = false;
            //             user.LockoutEnd = DateTime.MaxValue;
            //         }
            //         else
            //         {
            //             userProfile.IsActive = true;
            //             user.LockoutEnd = DateTime.UtcNow;
            //         }
            //         await UserManagementDbContext.SaveChangesAsync();
            //         await CoreDbContext.SaveChangesAsync();
            //     }
            // }
            return Unit.Value;
        }
    }
}
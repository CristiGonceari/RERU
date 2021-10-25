using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.EditUser {
    public class EditUserCommandHandler : BaseHandler, IRequestHandler<EditUserCommand, Unit> {
        private readonly UserManager<ERPIdentityUser> _userManager;
        public EditUserCommandHandler (ICommonServiceProvider commonServicepProvider, UserManager<ERPIdentityUser> userManager) : base (commonServicepProvider) {
            _userManager = userManager;
        }

        public async Task<Unit> Handle (EditUserCommand request, CancellationToken cancellationToken) {
            // var user = await UserManagementDbContext.Users.FirstOrDefaultAsync (u => u.Id == request.User.Id);
            // Mapper.Map (request.User, user);
            // await UserManagementDbContext.SaveChangesAsync ();

            // var userProfile = await CoreDbContext.UserProfiles.FirstOrDefaultAsync(u => u.UserId == request.User.Id);
            // if (userProfile == null)
            // {
            //     userProfile = new Data.Entities.UserProfile();
            //     CoreDbContext.UserProfiles.Add(userProfile);
            //     await CoreDbContext.SaveChangesAsync();
            // }
            // userProfile.UserId = user.Id;
            // userProfile.Name = user.Name;
            // userProfile.LastName = user.LastName;
            // userProfile.Email = user.Email;
            // await CoreDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
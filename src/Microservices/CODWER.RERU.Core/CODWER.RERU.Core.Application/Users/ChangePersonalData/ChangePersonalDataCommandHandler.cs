using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Users.ChangePersonalData
{
    public class ChangePersonalDataCommandHandler : BaseHandler, IRequestHandler<ChangePersonalDataCommand, Unit>
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly ICurrentApplicationUserProvider _userProvider;

        public ChangePersonalDataCommandHandler(ICommonServiceProvider commonServiceProvider, ICurrentApplicationUserProvider userProvider, UserManager<ERPIdentityUser> userManager) : base(commonServiceProvider)
        {
            _userManager = userManager;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(ChangePersonalDataCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProvider.Get();

            var userProfile = await AppDbContext
                .UserProfiles
                .FirstOrDefaultAsync(up => up.Id == int.Parse(currentUser.Id));

            if (userProfile == null)
            {
                userProfile = new UserProfile();
                AppDbContext.UserProfiles.Add(userProfile);
                await AppDbContext.SaveChangesAsync();
            }

            userProfile.FirstName = request.User.FirstName;
            userProfile.LastName = request.User.LastName;
            await AppDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
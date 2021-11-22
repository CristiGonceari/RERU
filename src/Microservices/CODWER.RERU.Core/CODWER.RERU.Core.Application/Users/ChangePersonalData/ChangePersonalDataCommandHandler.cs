using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.ChangePersonalData
{
    public class ChangePersonalDataCommandHandler : BaseHandler, IRequestHandler<ChangePersonalDataCommand, Unit>
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly ICurrentApplicationUserProvider _userProvider;

        public ChangePersonalDataCommandHandler(ICommonServiceProvider commonServicepProvider, ICurrentApplicationUserProvider userProvider, UserManager<ERPIdentityUser> userManager) : base(commonServicepProvider)
        {
            _userManager = userManager;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(ChangePersonalDataCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProvider.Get();

            var userProfile = await CoreDbContext
                .UserProfiles
                .FirstOrDefaultAsync(up => up.Id == int.Parse(currentUser.Id));

            if (userProfile == null)
            {
                userProfile = new Data.Entities.UserProfile();
                CoreDbContext.UserProfiles.Add(userProfile);
                await CoreDbContext.SaveChangesAsync();
            }

            userProfile.Name = request.User.Name;
            userProfile.LastName = request.User.LastName;
            await CoreDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
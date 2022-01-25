using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Identity.Models;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.GetPersonalData {
    public class GetPersonalDataQueryHandler : BaseHandler, IRequestHandler<GetPersonalDataQuery, UserPersonalDataDto> 
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly ICurrentApplicationUserProvider _userProvider;

        public GetPersonalDataQueryHandler (ICommonServiceProvider commonServiceProvider,
            ICurrentApplicationUserProvider userProvider,
            UserManager<ERPIdentityUser> userManager) : base (commonServiceProvider) 
        {
            _userManager = userManager;
            _userProvider = userProvider;
        }
        public async Task<UserPersonalDataDto> Handle (GetPersonalDataQuery request, CancellationToken cancellationToken) {
            var currentUser = await _userProvider.Get ();

            var user = await UserManagementDbContext.Users
                .FirstOrDefaultAsync (u => u.Email == currentUser.Email);

            await UserManagementDbContext.SaveChangesAsync ();

            return Mapper.Map<UserPersonalDataDto> (user);
        }
    }
}
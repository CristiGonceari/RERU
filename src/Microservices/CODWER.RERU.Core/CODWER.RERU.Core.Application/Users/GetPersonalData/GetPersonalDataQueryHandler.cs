using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Identity.Models;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.GetPersonalData {
    public class GetPersonalDataQueryHandler : BaseHandler, IRequestHandler<GetPersonalDataQuery, UserPersonalDataDto> 
    {
        private readonly ICurrentApplicationUserProvider _userProvider;

        public GetPersonalDataQueryHandler (ICommonServiceProvider commonServiceProvider, ICurrentApplicationUserProvider userProvider) : base (commonServiceProvider) 
        {
            _userProvider = userProvider;
        }

        public async Task<UserPersonalDataDto> Handle (GetPersonalDataQuery request, CancellationToken cancellationToken) 
        {
            var currentUser = await _userProvider.Get ();

            var user = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync (u => u.Id == int.Parse(currentUser.Id));

            await AppDbContext.SaveChangesAsync ();

            return Mapper.Map<UserPersonalDataDto> (user);
        }
    }
}
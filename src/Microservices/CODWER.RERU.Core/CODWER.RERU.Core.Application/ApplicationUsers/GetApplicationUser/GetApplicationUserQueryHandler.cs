using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.ApplicationUsers.GetApplicationUser
{
    public class GetApplicationUserQueryHandler : BaseHandler, IRequestHandler<GetApplicationUserQuery, ApplicationUser>
    {
        private readonly IApplicationUserProvider _applicationUserProvider;

        public GetApplicationUserQueryHandler(ICommonServiceProvider commonServiceProvider, IApplicationUserProvider applicationUserProvider)
            : base(commonServiceProvider)
        {
            _applicationUserProvider = applicationUserProvider;
        }
        public async Task<ApplicationUser> Handle(GetApplicationUserQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                return await _applicationUserProvider.Get(request.Id);
            }

            var userProfile = await CoreDbContext.UserProfiles
                .IncludeBasic()
                .FirstOrDefaultAsync(up => up.Id == request.UserProfileId);

            return Mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
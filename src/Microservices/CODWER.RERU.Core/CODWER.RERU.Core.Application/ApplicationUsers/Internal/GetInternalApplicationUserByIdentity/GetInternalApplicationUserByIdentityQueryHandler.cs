using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.ServiceProvider.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.ApplicationUsers.Internal.GetInternalApplicationUserByIdentity
{
    public class GetInternalApplicationUserByIdentityQueryHandler : BaseHandler, IRequestHandler<GetInternalApplicationUserByIdentityQuery, ApplicationUser>
    {
        public GetInternalApplicationUserByIdentityQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<ApplicationUser> Handle(GetInternalApplicationUserByIdentityQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles
             .IncludeBasic()
             .FirstOrDefaultAsync(up => up.Identities.Any(upi => upi.Identificator == request.Identity 
                                                                 && upi.Type == request.IdentityType));

            return Mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
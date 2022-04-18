using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CVU.ERP.Module.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.ApplicationUsers.Internal.GetInternalApplicationUser
{
    public class GetInternalApplicationUserQueryHandler : BaseHandler, IRequestHandler<GetInternalApplicationUserQuery, ApplicationUser>
    {
        public GetInternalApplicationUserQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<ApplicationUser> Handle(GetInternalApplicationUserQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles
             .IncludeBasic()
             .FirstOrDefaultAsync(up => up.Id == request.Id);

            return Mapper.Map<ApplicationUser>(userProfile);
        }
    }
}
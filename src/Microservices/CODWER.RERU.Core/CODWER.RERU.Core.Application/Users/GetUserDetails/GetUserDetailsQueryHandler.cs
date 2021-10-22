using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.GetUserDetails {
    public class GetUserDetailsQueryHandler : BaseHandler, IRequestHandler<GetUserDetailsQuery, UserDetailsOverviewDto> {
        public GetUserDetailsQueryHandler (ICommonServiceProvider commonServicepProvider) : base (commonServicepProvider) { }

        public async Task<UserDetailsOverviewDto> Handle (GetUserDetailsQuery request, CancellationToken cancellationToken) {
            var user = await CoreDbContext.UserProfiles
                .Where (d => d.Id == request.Id)
                .FirstOrDefaultAsync ();

            return Mapper.Map<UserDetailsOverviewDto> (user);
        }
    }
}
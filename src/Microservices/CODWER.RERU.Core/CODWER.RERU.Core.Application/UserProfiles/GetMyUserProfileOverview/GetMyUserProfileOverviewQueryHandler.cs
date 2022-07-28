using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider;

namespace CODWER.RERU.Core.Application.UserProfiles.GetMyUserProfileOverview
{
    public class GetMyUserProfileOverviewQueryHandler : BaseHandler, IRequestHandler<GetMyUserProfileOverviewQuery, UserProfileOverviewDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public GetMyUserProfileOverviewQueryHandler(
            ICommonServiceProvider commonServiceProvider, 
            ICurrentApplicationUserProvider currentApplicationUserProvider) : base(commonServiceProvider)
        {
            _currentUserProvider = currentApplicationUserProvider;
        }

        public async Task<UserProfileOverviewDto> Handle(GetMyUserProfileOverviewQuery request, CancellationToken cancellationToken)
        {
            var currentApplicationUser = await _currentUserProvider.Get();
            var userProfile = await AppDbContext.UserProfiles
                .IncludeBasic()
                .Include(x => x.Department)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(up => up.Id == int.Parse(currentApplicationUser.Id));

            var userProfDto = Mapper.Map<UserProfileOverviewDto>(userProfile);

            return userProfDto;
        }
    }
} 
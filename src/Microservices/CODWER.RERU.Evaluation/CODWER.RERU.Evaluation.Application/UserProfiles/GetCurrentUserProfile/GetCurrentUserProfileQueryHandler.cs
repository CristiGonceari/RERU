using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetCurrentUserProfile
{
    public class GetCurrentUserProfileQueryHandler : IRequestHandler<GetCurrentUserProfileQuery, UserProfileDto>
    {
        private readonly IUserProfileService _userProfileService;
        public GetCurrentUserProfileQueryHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<UserProfileDto> Handle(GetCurrentUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await _userProfileService.GetCurrentUser();
        }
    }
}

using CODWER.RERU.Evaluation.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetCurrentUserId
{
    public class GetCurrentUserIdQueryHandler : IRequestHandler<GetCurrentUserIdQuery, int>
    {
        private readonly IUserProfileService _userProfileService;

        public GetCurrentUserIdQueryHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<int> Handle(GetCurrentUserIdQuery request, CancellationToken cancellationToken)
        {
            var myProfile = await _userProfileService.GetCurrentUser();

            return myProfile.Id;
        }
    }
}

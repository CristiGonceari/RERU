using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetCurrentUserProfile
{
    public class GetCurrentUserProfileQuery : IRequest<UserProfileDto>
    {
    }
}

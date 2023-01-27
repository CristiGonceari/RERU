using CODWER.RERU.Evaluation360.DataTransferObjects.UserProfile;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.UserProfiles.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<UserProfileDto>
    {
    }
}

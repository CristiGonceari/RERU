using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public int Id { set; get; }
    }
}

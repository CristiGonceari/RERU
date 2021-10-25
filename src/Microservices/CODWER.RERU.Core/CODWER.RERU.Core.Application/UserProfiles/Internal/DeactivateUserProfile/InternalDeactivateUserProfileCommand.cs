using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.DeactivateUserProfile
{
    public class InternalDeactivateUserProfileCommand : IRequest<Unit>
    {
        public InternalDeactivateUserProfileCommand(int userProdileId)
        {
            UserProfileId = UserProfileId;
        }
        public int UserProfileId { set; get; }
    }
}
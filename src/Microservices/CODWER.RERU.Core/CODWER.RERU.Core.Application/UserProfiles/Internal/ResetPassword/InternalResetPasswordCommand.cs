using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.ResetPassword
{
    public class InternalResetPasswordCommand : IRequest<Unit>
    {
        public InternalResetPasswordCommand(int userProfileId)
        {
            UserProfileId = userProfileId;
        }

        public int UserProfileId { set; get; }
    }
}
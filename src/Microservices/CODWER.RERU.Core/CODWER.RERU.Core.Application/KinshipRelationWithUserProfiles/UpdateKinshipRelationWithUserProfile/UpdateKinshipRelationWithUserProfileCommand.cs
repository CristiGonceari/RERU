using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile
{
    public class UpdateKinshipRelationWithUserProfileCommand : IRequest<Unit>
    {
        public UpdateKinshipRelationWithUserProfileCommand(KinshipRelationWithUserProfileDto data)
        {
            Data = data;
        }

        public KinshipRelationWithUserProfileDto Data { get; set; }
    }
}

using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithUserProfile;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile
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

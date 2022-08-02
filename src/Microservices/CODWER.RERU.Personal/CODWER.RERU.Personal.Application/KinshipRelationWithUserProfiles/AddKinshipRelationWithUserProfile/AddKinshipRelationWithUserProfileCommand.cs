using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithUserProfile;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile
{
    public class AddKinshipRelationWithUserProfileCommand : IRequest<int>
    {
        public AddKinshipRelationWithUserProfileCommand(KinshipRelationWithUserProfileDto data)
        {
            Data = data;
        }

        public KinshipRelationWithUserProfileDto Data { get; set; }
    }
}

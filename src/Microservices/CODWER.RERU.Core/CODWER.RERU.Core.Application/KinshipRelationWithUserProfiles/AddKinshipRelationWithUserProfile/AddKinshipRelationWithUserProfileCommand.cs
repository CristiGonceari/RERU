using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile
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

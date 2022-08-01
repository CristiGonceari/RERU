using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.RemoveKinshipRelationWithUserProfile
{
    public class RemoveKinshipRelationWithUserProfileCommand : IRequest<Unit>
    {

        public int Id { get; set; }
    }
}

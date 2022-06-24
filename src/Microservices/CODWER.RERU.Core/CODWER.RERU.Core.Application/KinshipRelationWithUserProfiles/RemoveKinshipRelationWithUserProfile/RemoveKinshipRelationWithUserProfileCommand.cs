using MediatR;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.RemoveKinshipRelationWithUserProfile
{
    public class RemoveKinshipRelationWithUserProfileCommand : IRequest<Unit>
    {

        public int Id { get; set; }
    }
}

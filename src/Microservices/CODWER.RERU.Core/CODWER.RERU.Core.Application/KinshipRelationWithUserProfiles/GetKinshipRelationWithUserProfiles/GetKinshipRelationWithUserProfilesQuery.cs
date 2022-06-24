using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.GetKinshipRelationWithUserProfiles
{
    public class GetKinshipRelationWithUserProfilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<KinshipRelationWithUserProfileDto>>
    {
        public int UserProfileId { get; set; }
    }
}

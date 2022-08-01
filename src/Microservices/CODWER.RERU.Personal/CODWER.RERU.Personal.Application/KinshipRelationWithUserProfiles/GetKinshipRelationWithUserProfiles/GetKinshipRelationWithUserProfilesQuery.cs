using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.GetKinshipRelationWithUserProfiles
{
    public class GetKinshipRelationWithUserProfilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<KinshipRelationWithUserProfileDto>>
    {
        public int ContractorId { get; set; }
    }
}

using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudy;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.GetUserProfileRecommendationForStudy
{
    public class GetUserProfileRecommendationForStudyQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RecommendationForStudyDto>>
    {
        public int UserProfileId { get; set; }
    }
}

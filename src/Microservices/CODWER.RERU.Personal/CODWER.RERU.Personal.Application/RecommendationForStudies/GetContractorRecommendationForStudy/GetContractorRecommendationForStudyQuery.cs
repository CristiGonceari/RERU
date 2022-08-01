using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.GetContractorRecommendationForStudy
{
    public class GetContractorRecommendationForStudyQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RecommendationForStudyDto>>
    {
        public int ContractorId { get; set; }
    }
}

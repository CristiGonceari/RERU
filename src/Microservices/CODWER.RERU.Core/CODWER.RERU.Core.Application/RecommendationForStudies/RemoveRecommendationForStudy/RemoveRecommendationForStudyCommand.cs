using MediatR;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.RemoveRecommendationForStudy
{
    public class RemoveRecommendationForStudyCommand : IRequest<Unit>
    {
        public int RecommendationForStudyId { get; set; }
    }
}

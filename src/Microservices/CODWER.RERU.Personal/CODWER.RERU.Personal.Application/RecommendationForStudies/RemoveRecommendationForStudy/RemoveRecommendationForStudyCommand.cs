using MediatR;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.RemoveRecommendationForStudy
{
    public class RemoveRecommendationForStudyCommand : IRequest<Unit>
    {
        public int RecommendationForStudyId { get; set; }
    }
}

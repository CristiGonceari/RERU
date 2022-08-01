using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using MediatR;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.UpdateRecommendationForStudy
{
    public class UpdateRecommendationForStudyCommand : IRequest<Unit>
    {
        public UpdateRecommendationForStudyCommand(RecommendationForStudyDto data)
        {
            Data = data;
        }

        public RecommendationForStudyDto Data { get; set; }
    }
}

using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudyDto;
using MediatR;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.UpdateRecommendationForStudy
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

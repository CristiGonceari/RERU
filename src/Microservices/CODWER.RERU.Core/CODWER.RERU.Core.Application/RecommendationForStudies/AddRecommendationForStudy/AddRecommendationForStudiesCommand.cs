using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudy;
using MediatR;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.AddRecommendationForStudy
{
    public class AddRecommendationForStudiesCommand : IRequest<int>
    {
        public AddRecommendationForStudiesCommand(RecommendationForStudyDto data)
        {
            Data = data;
        }

        public RecommendationForStudyDto Data { get; set; }
    }
}

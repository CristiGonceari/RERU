using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using MediatR;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.AddRecommendationForStudy
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

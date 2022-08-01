using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.BulkAddEditRecommendationForStudy
{
    public class BulkAddEditRecommendationForStudyCommand : IRequest<Unit>
    {
        public BulkAddEditRecommendationForStudyCommand(List<RecommendationForStudyDto> list)
        {
            Data = list;
        }

        public List<RecommendationForStudyDto> Data { get; set; }
    }
}

﻿using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudyDto;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.BulkAddEditRecommendationForStudy
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
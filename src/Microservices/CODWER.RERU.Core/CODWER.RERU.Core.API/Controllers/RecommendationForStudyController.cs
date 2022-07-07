﻿using CODWER.RERU.Core.Application.RecommendationForStudies.AddRecommendationForStudy;
using CODWER.RERU.Core.Application.RecommendationForStudies.GetUserProfileRecommendationForStudy;
using CODWER.RERU.Core.Application.RecommendationForStudies.RemoveRecommendationForStudy;
using CODWER.RERU.Core.Application.RecommendationForStudies.UpdateRecommendationForStudy;
using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudyDto;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationForStudyController : BaseController
    {
        public RecommendationForStudyController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<RecommendationForStudyDto>> GetUserProfileRecommendationForStudy([FromQuery] GetUserProfileRecommendationForStudyQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateRecommendationForStudy([FromBody] RecommendationForStudyDto dto)
        {
            var command = new AddRecommendationForStudiesCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateRecommendationForStudy([FromBody] RecommendationForStudyDto dto)
        {
            var command = new UpdateRecommendationForStudyCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveRecommendationForStudy([FromRoute] int id)
        {
            var command = new RemoveRecommendationForStudyCommand { RecommendationForStudyId = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
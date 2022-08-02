using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.RecommendationForStudies.AddRecommendationForStudy;
using CODWER.RERU.Personal.Application.RecommendationForStudies.BulkAddEditRecommendationForStudy;
using CODWER.RERU.Personal.Application.RecommendationForStudies.GetContractorRecommendationForStudy;
using CODWER.RERU.Personal.Application.RecommendationForStudies.RemoveRecommendationForStudy;
using CODWER.RERU.Personal.Application.RecommendationForStudies.UpdateRecommendationForStudy;
using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationForStudyController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<RecommendationForStudyDto>> GetUserProfileRecommendationForStudy([FromQuery] GetContractorRecommendationForStudyQuery query)
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

        [HttpPut("bulk-import")]
        public async Task<Unit> AddEditStudies([FromBody] List<RecommendationForStudyDto> dtoList)
        {
            var command = new BulkAddEditRecommendationForStudyCommand(dtoList);

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

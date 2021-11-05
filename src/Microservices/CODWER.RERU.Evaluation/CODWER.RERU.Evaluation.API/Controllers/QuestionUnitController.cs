using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus;
using CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnits;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetTags;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionUnitController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<QuestionUnitDto> GetQuestionUnit([FromRoute] int id)
        {
            var query = new GetQuestionUnitQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<QuestionUnitDto>> GetQuestionUnits([FromQuery] GetQuestionUnitsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddQuestionUnit([FromBody] AddQuestionUnitCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<Unit> EditQuestionUnit([FromBody] EditQuestionUnitCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteQuestionUnit([FromRoute] int id)
        {
            var command = new DeleteQuestionUnitCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpGet("tags")]
        public async Task<List<TagDto>> GetTags([FromQuery] GetTagsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch("edit-status")]
        public async Task<Unit> EditQuestionStatus([FromBody] EditQuestionStatusCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

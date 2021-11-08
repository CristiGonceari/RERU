using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.BulkUploadQuestionUnits;
using CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus;
using CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionBulkTemplate;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnit;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnits;
using CODWER.RERU.Evaluation.Application.QuestionUnits.GetTags;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;

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

        [IgnoreResponseWrap]
        [HttpGet("excel-template/{questionType}")]
        public async Task<FileContentResult> GetExcelTemplate([FromRoute] QuestionTypeEnum questionType)
        {
            byte[] answerBytes = await Mediator.Send(new GetQuestionBulkTemplateQuery { QuestionType = questionType }) as byte[];
            return File(answerBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AddQuestionsTemplate.xlsx");
        }

        [IgnoreResponseWrap]
        [HttpPost("excel-template/upload")]
        public async Task<FileContentResult> BulkQuestionsUpload([FromForm] IFormFile file)
        {
            byte[] answerBytes = await Mediator.Send(new BulkUploadQuestionUnitsCommand { Input = file }) as byte[];
            return File(answerBytes ?? new byte[0], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ErrorList.xlsx");
        }
    }
}

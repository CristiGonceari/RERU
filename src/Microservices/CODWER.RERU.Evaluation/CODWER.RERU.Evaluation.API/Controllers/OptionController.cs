using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Options.AddOption;
using CODWER.RERU.Evaluation.Application.Options.BulkUploadOptions;
using CODWER.RERU.Evaluation.Application.Options.DeleteAllOptionsByQuestion;
using CODWER.RERU.Evaluation.Application.Options.DeleteOption;
using CODWER.RERU.Evaluation.Application.Options.EditOption;
using CODWER.RERU.Evaluation.Application.Options.GetOption;
using CODWER.RERU.Evaluation.Application.Options.GetOptionBulkTemplate;
using CODWER.RERU.Evaluation.Application.Options.GetOptions;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<OptionDto> GetOption([FromRoute] int id)
        {
            var query = new GetOptionQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet()]
        public async Task<List<OptionDto>> GetOptions([FromQuery] GetOptionsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddOption([FromForm] AddOptionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<Unit> EditOption([FromForm] EditOptionsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteOption([FromRoute] int id)
        {
            var command = new DeleteOptionCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpDelete("all/{questionId}")]
        public async Task<Unit> DeleteAllOptionsByQuestion([FromRoute] int questionId)
        {
            return await Mediator.Send(new DeleteAllOptionsByQuestionCommand { QuestionUnitId = questionId });
        }

        [IgnoreResponseWrap]
        [HttpGet("excel-template/{questionType}")]
        public async Task<FileContentResult> GetExcelTemplate([FromRoute] QuestionTypeEnum questionType)
        {
            byte[] answerBytes = await Mediator.Send(new GetOptionBulkTemplateQuery { QuestionType = questionType }) as byte[];
            return File(answerBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AddOptionTemplate.xlsx");
        }

        [IgnoreResponseWrap]
        [HttpPost("excel-template/upload")]
        public async Task<FileContentResult> BulkOptionsUpload([FromForm] BulkUploadOptionsCommand command)
        {
            byte[] answerBytes = await Mediator.Send(command) as byte[];
            return File(answerBytes ?? new byte[0], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ErrorList.xlsx");
        }
       

    }
}

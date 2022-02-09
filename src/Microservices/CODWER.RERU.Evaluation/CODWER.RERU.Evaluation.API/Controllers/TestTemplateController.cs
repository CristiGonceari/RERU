using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateSettings;
using CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplateRules;
using CODWER.RERU.Evaluation.Application.TestTemplates.CloneTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplates.DeleteTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplateStatus;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetQuestionsPreview;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateRules;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplates;
using CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateSettings;
using CODWER.RERU.Evaluation.Application.TestTemplates.PrintTestTemplates;
using CODWER.RERU.Evaluation.Application.TestTemplates.ValidateTestTemplate;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTemplateController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<TestTemplateDto> GetTestType([FromRoute] int id)
        {
            var query = new GetTestTemplateQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<TestTemplateDto>> GetTestTypes([FromQuery] GetTestTemplatesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddTestType([FromBody] AddTestTemplateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<int> EditTestType([FromBody] EditTestTemplateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteTestType([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteTestTemplateCommand { Id = id });
        }

        [HttpGet("settings")]
        public async Task<TestTemplateSettingsDto> GetTestTypeSettings([FromQuery] GetTestTemplateSettingsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("rules/{id}")]
        public async Task<RulesDto> GetTestTypeRules([FromRoute] int id)
        {
            var query = new GetTestTemplateRulesQuery() { TestTypeId = id };
            return await Mediator.Send(query);
        }

        [HttpGet("status")]
        public async Task<List<SelectTestTemplateValueDto>> GetTestTypeByStatus([FromQuery] GetTestTemplateByStatusQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch("settings")]
        public async Task<Unit> ChangeTestTypeSettings([FromBody] AddEditTestTemplateSettingsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("rules")]
        public async Task<Unit> AddRulesToTestType([FromBody] AddTestTemplateRulesCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("status")]
        public async Task<Unit> ChangeTestTypeStatus([FromBody] EditTestTemplateStatusCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("questions-preview")]
        public async Task<List<QuestionUnitPreviewDto>> GetQuestionsPreview([FromQuery] GetQuestionsPreviewQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("validate")]
        public async Task<Unit> ValidateTestType([FromQuery] ValidateTestTemplateQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("clone")]
        public async Task<int> CloneTestType([FromBody] CloneTestTemplateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintTestTypes([FromBody] PrintTestTemplatesCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
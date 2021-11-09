using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestTypes.AddEditTestTypeSettings;
using CODWER.RERU.Evaluation.Application.TestTypes.AddTestType;
using CODWER.RERU.Evaluation.Application.TestTypes.AddTestTypeRules;
using CODWER.RERU.Evaluation.Application.TestTypes.ChangeTestTypeStatus;
using CODWER.RERU.Evaluation.Application.TestTypes.CloneTestType;
using CODWER.RERU.Evaluation.Application.TestTypes.DeleteTestType;
using CODWER.RERU.Evaluation.Application.TestTypes.EditTestType;
using CODWER.RERU.Evaluation.Application.TestTypes.GetQuestionsPreview;
using CODWER.RERU.Evaluation.Application.TestTypes.GetTestType;
using CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeByStatus;
using CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeRules;
using CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypes;
using CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeSettings;
using CODWER.RERU.Evaluation.Application.TestTypes.ValidateTestType;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTypeController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<TestTypeDto> GetTestType([FromRoute] int id)
        {
            var query = new GetTestTypeQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<TestTypeDto>> GetTestTypes([FromQuery] GetTestTypesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddTestType([FromBody] AddTestTypeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<int> EditTestType([FromBody] EditTestTypeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Unit> DeleteTestType([FromQuery] DeleteTestTypeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("settings")]
        public async Task<TestTypeSettingsDto> GetTestTypeSettings([FromQuery] GetTestTypeSettingsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("rules")]
        public async Task<RulesDto> GetTestTypeRules([FromRoute] int id)
        {
            var query = new GetTestTypeRulesQuery() { TestTypeId = id };
            return await Mediator.Send(query);
        }

        [HttpGet("status/{status}")]
        public async Task<List<SelectItem>> GetTestTypeByStatus([FromRoute] TestTypeStatusEnum status)
        {
            var query = new GetTestTypeByStatusQuery() { TestTypeStatus = status };
            return await Mediator.Send(query);
        }

        [HttpPatch("settings")]
        public async Task<Unit> ChangeTestTypeSettings([FromBody] AddEditTestTypeSettingsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("rules")]
        public async Task<Unit> AddRulesToTestType([FromBody] AddTestTypeRulesCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("status")]
        public async Task<Unit> ChangeTestTypeStatus([FromBody] ChangeTestTypeStatusCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("questions-preview")]
        public async Task<List<QuestionUnitPreviewDto>> GetQuestionsPreview([FromQuery] GetQuestionsPreviewQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("validate")]
        public async Task<Unit> ValidateTestType([FromQuery] ValidateTestTypeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("clone")]
        public async Task<int> CloneTestType([FromBody] CloneTestTypeCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
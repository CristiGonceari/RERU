using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.References.GetEventsValues;
using CODWER.RERU.Evaluation.Application.References.GetQuestionCategoryValue;
using CODWER.RERU.Evaluation.Application.References.GetTestTemplatesValue;
using CODWER.RERU.Evaluation.Application.References.GetUsersValue;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : BaseController
    {
        [HttpGet("question-types-value/select-values")]
        public async Task<List<SelectItem>> GetQuestionTypes()
        {
            var items = EnumConverter<QuestionTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("test-template/select-values")]
        public async Task<List<SelectItem>> GetTestTemplates()
        {
            var query = new GetTestTemplatesValueQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("question-categories-value/select-values")]
        public async Task<List<SelectItem>> GetQuestionCategories()
        {
            var query = new GetQuestionCategoryValueQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("test-type-statuses/select-values")]
        public async Task<List<SelectItem>> GetTestTemplateStatueses()
        {
            var items = EnumConverter<TestTemplateStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("test-type-mode/select-values")]
        public async Task<List<SelectItem>> GetMode()
        {
            var items = EnumConverter<TestTemplateModeEnum>.SelectValues;

            return items;
        }

        [HttpGet("locations/select-values")]
        public async Task<List<SelectItem>> GetLocationType()
        {
            var items = EnumConverter<TestingLocationType>.SelectValues;

            return items;
        }

        [HttpGet("test-statuses/select-values")]
        public async Task<List<SelectItem>> GetTestStatuses()
        {
            var items = EnumConverter<TestStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("users-value/select-values")]
        public async Task<List<SelectItem>> GetUsers([FromQuery] GetUsersValueQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("events-value/select-values")]
        public async Task<List<SelectEventValueDto>> GetEvents()
        {
            var query = new GetEventsValueQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("statistics/select-values")]
        public async Task<List<SelectItem>> GetStatisticEnum()
        {
            var items = EnumConverter<StatisticsQuestionFilterEnum>.SelectValues;

            return items;
        }

        [HttpGet("question-status/select-values")]
        public List<SelectItem> GetQuestionStatuses()
        {
            var items = EnumConverter<QuestionUnitStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("score-formula/select-values")]
        public List<SelectItem> GetScoreFormulas()
        {
            var items = EnumConverter<ScoreFormulaEnum>.SelectValues;

            return items;
        }

        [HttpGet("solicited-test-status/select-values")]
        public List<SelectItem> GetSolicitedTestStatus()
        {
            var items = EnumConverter<SolicitedTestStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("document-template-type/select-values")]
        public async Task<List<SelectItem>> GetDocumentTemplateType()
        {
            var items = EnumConverter<FileTypeEnum>.SelectValues;

            return items;
        }
    }
}

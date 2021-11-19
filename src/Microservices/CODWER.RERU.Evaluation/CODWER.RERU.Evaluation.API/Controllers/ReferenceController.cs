using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.References.GetEventsValues;
using CODWER.RERU.Evaluation.Application.References.GetQuestionCategoryValue;
using CODWER.RERU.Evaluation.Application.References.GetUsersValue;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;
using CODWER.RERU.Evaluation.Application.References.GetTestTypeValue;

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

        [HttpGet("test-type/select-values")]
        public async Task<List<SelectItem>> GetTestTypes()
        {
            var query = new GetTestTypeValueQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("question-categories-value/select-values")]
        public async Task<List<SelectItem>> GetQuestionCategories()
        {
            var query = new GetQuestionCategoryValueQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("test-type-statuses/select-values")]
        public async Task<List<SelectItem>> GetTestTypeStatueses()
        {
            var items = EnumConverter<TestTypeStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("test-type-mode/select-values")]
        public async Task<List<SelectItem>> GetMode()
        {
            var items = EnumConverter<TestTypeModeEnum>.SelectValues;

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
    }
}

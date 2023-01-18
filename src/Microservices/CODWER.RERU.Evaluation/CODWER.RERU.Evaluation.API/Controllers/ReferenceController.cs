using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.References.GetEventsValues;
using CODWER.RERU.Evaluation.Application.References.GetQuestionCategoryValue;
using CODWER.RERU.Evaluation.Application.References.GetTestTemplatesValue;
using CODWER.RERU.Evaluation.Application.References.GetUsersValue;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.References.GetAddTestProcesses;
using CODWER.RERU.Evaluation.Application.References.GetDepartmentsValue;
using CODWER.RERU.Evaluation.Application.References.GetEvaluationRoles;
using CODWER.RERU.Evaluation.Application.References.GetEventLocationValue;
using CODWER.RERU.Evaluation.Application.References.GetRequiredDocumentsValue;
using CODWER.RERU.Evaluation.Application.References.GetRolesValue;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Users;
using CODWER.RERU.Evaluation.Application.References.GetAllEventsValues;
using CODWER.RERU.Evaluation.Application.References.GetLocationsSelectValues;
using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;

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

        [HttpGet("test-results/select-values")]
        public async Task<List<SelectItem>> GetTestResults()
        {
            var items = EnumConverter<TestResultStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("qualifying-results/select-values")]
        public async Task<List<SelectItem>> GetQualifyingResults()
        {
            var items = EnumConverter<QualifyingTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("users-value/select-values")]
        public async Task<List<SelectItem>> GetUsers([FromQuery] GetUsersValueQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("processes-value/select-values")]
        public async Task<List<ProcessDataDto>> GetProcesses()
        {
            var query = new GetAddTestProcessesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("event-locations/select-values")]
        public async Task<List<LocationDto>> GetEventLocations([FromQuery] GetEventLocationValueQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("locations-event/select-values")]
        public async Task<List<SelectItem>> GetLocationsSelectValues([FromQuery] GetLocationsSelectValuesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("events-value/select-values")]
        public async Task<List<SelectEventValueDto>> GetEvents()
        {
            var query = new GetEventsValueQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("all-events-value/select-values")]
        public async Task<List<SelectEventValueDto>> GetAllEvents()
        {
            var query = new GetAllEventsValuesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("required-document/select-values")]
        public async Task<List<SelectItem>> GetRequiredDocument([FromQuery] GetRequiredDocumentsValueQuery query)
        {
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
            var items = EnumConverter<SolicitedPositionStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("document-template-type/select-values")]
        public async Task<List<SelectItem>> GetDocumentTemplateType()
        {
            var items = EnumConverter<FileTypeEnum>.SelectValues;

            var evaluationDocuments = items.Where(x => x.Label.Contains("test")).ToList();

            return evaluationDocuments;
        }

        [HttpGet("medical-enum/select-values")]
        public async Task<List<SelectItem>> GetMedicalColumnEnum()
        {
            var items = EnumConverter<MedicalColumnEnum>.SelectValues;

            return items;
        }

        [HttpGet("user-status/select-values")]
        public async Task<List<SelectItem>> GetUserEnum()
        {
            var items = EnumConverter<UserStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("article-roles/select-values")]
        public async Task<List<SelectItem>> GetArticleRoles()
        {
            var query = new GetEvaluationRolesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("departments/select-values")]
        public async Task<List<SelectItem>> GetDepartments()
        {
            var query = new GetDepartmentsValuesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("roles/select-values")]
        public async Task<List<SelectItem>> GetRoles()
        {
            var query = new GetRolesValuesQuery();

            return await Mediator.Send(query);
        }
    }
}

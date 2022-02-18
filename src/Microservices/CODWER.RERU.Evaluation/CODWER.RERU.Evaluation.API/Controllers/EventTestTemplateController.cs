using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using MediatR;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.GetEventTestTemplates;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.GetNoAssignedTestTemplates;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.AssignTestTemplateToEvent;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTemplateFromEvent;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTestTemplateController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<TestTemplateDto>> GetEventTestTemplates([FromQuery] GetEventTestTemplatesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<List<TestTemplateDto>> GetNoAssignedTestTypes([FromQuery] GetNoAssignedTestTemplatesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignTestTypeToEvent([FromBody] AssignTestTemplateToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&TestType={testTypeId}")]
        public async Task<Unit> UnassignTestTypeFromEvent([FromRoute] int eventId, int testTypeId)
        {
            var command = new UnassignTestTemplateFromEventCommand { EventId = eventId, TestTemplateId = testTypeId };

            return await Mediator.Send(command);
        }
    }
}

using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.AssignTestTemplateToEvent;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.GetEventTestTemplates;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.GetNoAssignedTestTemplates;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.GetTestTemplatesByEventsIds;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.PrintEventTestTemplates;
using CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTemplateFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<TestTemplateDto>> GetNoAssignedTestTemplates([FromQuery] GetNoAssignedTestTemplatesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("by-event")]
        public async Task<List<TestTemplatesByEventDto>> GetTestTemplatesByEventsIds([FromQuery] GetTestTemplatesByEventsIdsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignTestTemplateToEvent([FromBody] AssignTestTemplateToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&testTemplate={testTemplateId}")]
        public async Task<Unit> UnassignTestTemplateFromEvent([FromRoute] int eventId, int testTemplateId)
        {
            var command = new UnassignTestTemplateFromEventCommand { EventId = eventId, TestTemplateId = testTemplateId };

            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEventTestTemplates([FromBody] PrintEventTestTemplatesCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

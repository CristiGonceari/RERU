using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventTestTypes.AssignTestTypeToEvent;
using CODWER.RERU.Evaluation.Application.EventTestTypes.GetEventTestTypes;
using CODWER.RERU.Evaluation.Application.EventTestTypes.GetNoAssignedTestTypes;
using CODWER.RERU.Evaluation.Application.EventTestTypes.UnassignTestTypeFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTestTypeController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<TestTemplateDto>> GetEventTestTypes([FromQuery] GetEventTestTypesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<List<TestTemplateDto>> GetNoAssignedTestTypes([FromQuery] GetNoAssignedTestTypesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignTestTypeToEvent([FromBody] AssignTestTypeToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&TestType={testTypeId}")]
        public async Task<Unit> UnassignTestTypeFromEvent([FromRoute] int eventId, int testTypeId)
        {
            var command = new UnassignTestTypeFromEventCommand { EventId = eventId, TestTypeId = testTypeId };

            return await Mediator.Send(command);
        }
    }
}

using System.Collections.Generic;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.PlanEvents.AssignEventToPlan;
using CODWER.RERU.Evaluation.Application.PlanEvents.GetPlanEvents;
using CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.PlanEvents.GetNotAssignedEvents;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanEventController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<EventDto>> GetPlanEvents([FromQuery] GetPlanEventsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("not-assigned")]
        public async Task<List<EventDto>> GetEventsNotAssignedToPlan([FromQuery] GetNotAssignedEventsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignEventToPlan([FromBody] AssignEventToPlanCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<Unit> UnassignEventFromPlan([FromBody] UnassignPlanEventCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

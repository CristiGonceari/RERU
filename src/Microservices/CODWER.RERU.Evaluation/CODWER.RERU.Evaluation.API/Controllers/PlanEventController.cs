using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.PlanEvents.AssignEventToPlan;
using CODWER.RERU.Evaluation.Application.PlanEvents.GetPlanEvents;
using CODWER.RERU.Evaluation.Application.PlanEvents.GetPlanResponsiblePersons;
using CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent;
using CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanResponsiblePerson;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.PlanEvents.AssignResponsiblePersonToPlan;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanEventController : BaseController
    {

        [HttpGet("events-by-{id}")]
        public async Task<PaginatedModel<EventDto>> GetPlanEvents([FromRoute] int id)
        {
            var query = new GetPlanEventsQuery { PlanId = id };
            return await Mediator.Send(query);
        }

        [HttpPost("assign-event")]
        public async Task<Unit> AssignEventToPlan([FromBody] AssignEventToPlanCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("assign-person")]
        public async Task<Unit> AssignPersonToPlan([FromBody] AssignResponsiblePersonToPlanCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("unassign-event")]
        public async Task<Unit> DetachPlanEvent([FromBody] UnassignPlanEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("unassign-person")]
        public async Task<Unit> DetachPlanResponsiblePerson([FromBody] UnassignPlanResponsiblePersonCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("responsible-persons-by-{id}")]

        public async Task<PaginatedModel<UserProfileDto>> GetPlanResponsiblePersons([FromRoute] int id)
        {
            var query = new GetPlanResponsiblePersonsQuery {PlanId = id};
            return await Mediator.Send(query);
        }

    }
}

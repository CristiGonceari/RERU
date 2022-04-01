using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetEventEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetNoAssignedEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.UnassignEvaluatorFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetAssignedEvaluators;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventEvaluatorController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetEventEvaluators([FromQuery] GetEventEvaluatorsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<PaginatedModel<UserProfileDto>> GetNoAssignedEvaluators([FromQuery] GetNoAssignedEvaluatorsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<List<int>> AssignEvaluatorToEvent([FromBody] AssignEvaluatorToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&Evaluator={evaluatorId}")]
        public async Task<Unit> UnassignEvaluatorFromEvent([FromRoute] int eventId, int evaluatorId)
        {
            var command = new UnassignEvaluatorFromEventCommand { EventId = eventId, EvaluatorId = evaluatorId };

            return await Mediator.Send(command);
        }

        [HttpGet("assigned")]
        public async Task<PaginatedModel<UserProfileDto>> GetAssignedUsers([FromQuery] GetAssignedEvaluatorsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}

﻿using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetAssignedEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetEventEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetListOfEventEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.GetNoAssignedEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.PrintEventEvaluators;
using CODWER.RERU.Evaluation.Application.EventEvaluators.SendToAssignedEvaluatorNotifications;
using CODWER.RERU.Evaluation.Application.EventEvaluators.UnassignEvaluatorFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
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
        [HttpPut]
        public async Task<Unit> SendToAssignedEvaluatorNotification([FromBody] SendToAssignedEvaluatorNotificationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("list-of-event-evaluators")]
        public async Task<List<int>> GetListOfEventEvaluators([FromQuery] GetListOfEventEvaluatorsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEventEvaluator([FromBody] PrintEventEvaluatorsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent;
using CODWER.RERU.Evaluation.Application.EventUsers.GetEventUsers;
using CODWER.RERU.Evaluation.Application.EventUsers.GetNoAssignedUsers;
using CODWER.RERU.Evaluation.Application.EventUsers.UnassignUserFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using CODWER.RERU.Evaluation.Application.EventUsers.GetEventAssignedUsers;
using CODWER.RERU.Evaluation.Application.EventUsers.GetListOfEventUsers;
using CODWER.RERU.Evaluation.Application.EventUsers.PrintEventUsers;
using CODWER.RERU.Evaluation.Application.EventUsers.SendToAssignedUserNotifications;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventUserController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetEventUsers([FromQuery] GetEventUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("list-of-event-user")]
        public async Task<List<int>> GetListOfEventUsers([FromQuery] GetListOfEventUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<PaginatedModel<UserProfileDto>> GetNoAssignedUsers([FromQuery] GetNoAssignedUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<List<int>> AssignUserToEvent([FromBody] AssignUserToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<Unit> SendToAssignedUserNotification([FromBody] SendToAssignedUserNotificationsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&User={userProfileId}")]
        public async Task<Unit> UnassignUserFromEvent([FromRoute] int eventId, int userProfileId )
        {
            var command = new UnassignUserFromEventCommand {EventId = eventId, UserProfileId = userProfileId  };

            return await Mediator.Send(command);
        }

        [HttpGet("assigned")]
        public async Task<PaginatedModel<UserProfileDto>> GetNoAssignedUsers([FromQuery] GetEventAssignedUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEventUsers([FromBody] PrintEventUsersCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

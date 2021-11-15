using System.Collections.Generic;
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

        [HttpGet("no-assigned")]
        public async Task<List<UserProfileDto>> GetNoAssignedUsers([FromQuery] GetNoAssignedUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignUserToEvent([FromBody] AssignUserToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&User={userProfileId}")]
        public async Task<Unit> UnassignUserFromEvent([FromRoute] int eventId, int userProfileId )
        {
            var command = new UnassignUserFromEventCommand {EventId = eventId, UserProfileId = userProfileId  };

            return await Mediator.Send(command);
        }
    }
}

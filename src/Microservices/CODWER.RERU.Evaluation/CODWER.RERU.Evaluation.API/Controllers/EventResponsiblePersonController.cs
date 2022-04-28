using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetEventResponsiblePersons;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetNoAssignedResponsiblePersons;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.UnassignResponsiblePersonFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.SendToAssignedResponsiblePersonNotifications;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetListOfEventResponsiblePerson;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventResponsiblePersonController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetEventResponsiblePersons([FromQuery] GetEventResponsiblePersonsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<PaginatedModel<UserProfileDto>> GetNoAssignedResponsiblePersons([FromQuery] GetNoAssignedResponsiblePersonsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<List<int>> AssignResponsiblePersonToEvent([FromBody] AssignResponsiblePersonToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Event={eventId}&&UserProfile={userProfileId}")]
        public async Task<Unit> UnassignResponsiblePersonFromEvent([FromRoute] int eventId, int userProfileId )
        {
            var command = new UnassignResponsiblePersonFromEventCommand { EventId = eventId, UserProfileId = userProfileId };

            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<Unit> SendToAssignedResponsiblePersonNotifications([FromBody] SendToAssignedResponsiblePersonNotificationsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("list-of-event-responsible-person")]
        public async Task<List<int>> GetListOfEventResponsiblePerson([FromQuery] GetListOfEventResponsiblePersonQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}

using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetEventResponsiblePersons;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetListOfEventResponsiblePerson;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetNoAssignedResponsiblePersons;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.PrintEventResponsiblePersons;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.SendToAssignedResponsiblePersonNotifications;
using CODWER.RERU.Evaluation.Application.EventResponsiblePersons.UnassignResponsiblePersonFromEvent;
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

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEventLocations([FromBody] PrintEventResponsiblePersonsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

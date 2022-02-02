using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Events.AddEvent;
using CODWER.RERU.Evaluation.Application.Events.DeleteEvent;
using CODWER.RERU.Evaluation.Application.Events.EditEvent;
using CODWER.RERU.Evaluation.Application.Events.GetEvent;
using CODWER.RERU.Evaluation.Application.Events.GetEvents;
using CODWER.RERU.Evaluation.Application.Events.GetMyEvents;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.Application.Events.GetMyEventsByDate;
using CODWER.RERU.Evaluation.Application.Events.GetMyEventsCount;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Events.GetUserEvents;
using CODWER.RERU.Evaluation.Application.Events.PrintEvents;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<EventDto> GetEvent([FromRoute] int id)
        {
            var query = new GetEventQuery {Id = id};
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<EventDto>> GetEvents([FromQuery] GetEventsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-events")]
        public async Task<PaginatedModel<EventDto>> GetMyEvents([FromQuery] GetMyEventsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-events-by-date")]
        public async Task<PaginatedModel<EventDto>> GetMyEventsByDate([FromQuery] GetMyEventsByDateQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-events-count")]
        public async Task<List<EventCount>> GetMyEventsCount([FromQuery] GetMyEventsCountQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> CreateEvent([FromBody] AddEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<int> UpdateEvent([FromBody] EditEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteEvent([FromRoute] int id)
        {
            var command = new DeleteEventCommand {Id = id};
            return await Mediator.Send(command);
        }

        [HttpGet("user-events")]
        public async Task<PaginatedModel<EventDto>> GetUserEvents([FromQuery] GetUserEventsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEventsPdf([FromBody] PrintEventsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

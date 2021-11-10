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
using CVU.ERP.Evaluation.DataTransferObjects.Events;

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
    }
}

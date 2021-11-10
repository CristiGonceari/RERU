using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventLocations.AssignLocationToEvent;
using CODWER.RERU.Evaluation.Application.EventLocations.GetEventLocations;
using CODWER.RERU.Evaluation.Application.EventLocations.GetNoAssignedLocations;
using CODWER.RERU.Evaluation.Application.EventLocations.UnassignLocationFromEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLocationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<LocationDto>> GetEventLocations([FromQuery] GetEventLocationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<List<LocationDto>> GetNoAssignedLocations([FromQuery] GetNoAssignedLocationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignLocationToEvent([FromBody] AssignLocationToEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Unit> UnassignLocationFromEvent([FromBody] UnassignLocationFromEventCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

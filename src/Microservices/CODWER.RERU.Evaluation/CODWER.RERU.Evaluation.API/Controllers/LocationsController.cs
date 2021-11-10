using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Locations.AddLocation;
using CODWER.RERU.Evaluation.Application.Locations.AssignLocationComputer;
using CODWER.RERU.Evaluation.Application.Locations.DeleteLocation;
using CODWER.RERU.Evaluation.Application.Locations.EditLocation;
using CODWER.RERU.Evaluation.Application.Locations.GetEventLocations;
using CODWER.RERU.Evaluation.Application.Locations.GetLocation;
using CODWER.RERU.Evaluation.Application.Locations.GetLocationByComputer;
using CODWER.RERU.Evaluation.Application.Locations.GetLocationComputers;
using CODWER.RERU.Evaluation.Application.Locations.GetLocationDetails;
using CODWER.RERU.Evaluation.Application.Locations.GetLocations;
using CODWER.RERU.Evaluation.Application.Locations.GetLocationsNotAssignedToEvent;
using CODWER.RERU.Evaluation.Application.Locations.UnassignLocationComputer;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : BaseController
    {
        [HttpGet]
        public async Task<LocationDto> GetLocation([FromQuery] GetLocationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("details")]
        public async Task<LocationDto> GetLocationDetails([FromQuery] GetLocationDetailsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("list")]
        public async Task<PaginatedModel<LocationDto>> GetLocations([FromQuery] GetLocationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("by-event")]
        public async Task<List<LocationDto>> GetLocationsByEvent([FromQuery] GetEventLocationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("not-attached-event")]
        public async Task<List<LocationDto>> GetLocationsNotAssignedEvent([FromQuery] GetLocationsNotAssignedToEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("by-computer")]
        public async Task<LocationDto> GetLocationByComputer([FromQuery] GetLocationByComputerQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("computers")]
        public async Task<PaginatedModel<LocationClientDto>> GetLocationComputers([FromQuery] GetLocationComputersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("attach-computer")]
        public async Task<string> AttachLocationComputer([FromBody] AssignLocationComputerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("detach-computer")]
        public async Task<Unit> DetachLocationComputer([FromBody] UnassignLocationComputerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("create")]
        public async Task<int> CreateLocation([FromBody] AddLocationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<int> UpdateLocation([FromBody] EditLocationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Unit> DeleteLocation([FromQuery] DeleteLocationCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}

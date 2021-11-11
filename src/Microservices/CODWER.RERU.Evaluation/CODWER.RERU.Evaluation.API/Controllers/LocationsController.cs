using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Locations.AddLocation;
using CODWER.RERU.Evaluation.Application.Locations.DeleteLocation;
using CODWER.RERU.Evaluation.Application.Locations.EditLocation;
using CODWER.RERU.Evaluation.Application.Locations.GetLocation;
using CODWER.RERU.Evaluation.Application.Locations.GetLocations;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<LocationDto> GetLocation([FromRoute] int id)
        {
            return await Mediator.Send(new GetLocationQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<LocationDto>> GetLocations([FromQuery] GetLocationsQuery query)
        {
            return await Mediator.Send(query);
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

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteLocation([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteLocationCommand { Id = id });
        }
    }
}

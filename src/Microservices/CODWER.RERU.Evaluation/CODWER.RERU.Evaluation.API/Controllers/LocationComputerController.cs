using CODWER.RERU.Evaluation.Application.LocationComputers.GetLocationByComputer;
using CODWER.RERU.Evaluation.Application.LocationComputers.GetLocationComputers;
using CODWER.RERU.Evaluation.Application.LocationComputers.UnassignLocationComputer;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation.API.Config;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.LocationComputers.AssignLocationComputer;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationComputerController : BaseController
    {
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
    }
}

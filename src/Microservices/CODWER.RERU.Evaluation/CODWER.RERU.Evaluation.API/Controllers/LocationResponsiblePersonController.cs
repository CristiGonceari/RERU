using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetLocationResponsiblePersons;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationResponsiblePersonController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetLocationResponsiblePersons([FromQuery] GetLocationResponsiblePersonsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("assign-person")]
        public async Task<Unit> AssignPersonToLocation([FromBody] AssignResponsiblePersonToLocationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("detach-person")]
        public async Task<Unit> DetachLocationResponsiblePerson([FromBody] UnassignResponsiblePersonFromLocationCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

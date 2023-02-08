using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetLocationResponsiblePersons;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetNoAssinedResponsiblePersons;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.PrintLocationResponsiblePersons;
using CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("no-assigned")]
        public async Task<PaginatedModel<UserProfileDto>> GetUserProfilesNotAttachedToLocation([FromQuery] GetNoAssignedResponsiblePersonsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<List<int>> AssignPersonToLocation([FromBody] AssignResponsiblePersonToLocationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Location={locationId}&&UserProfile={personId}")]
        public async Task<Unit> UnassignResponsiblePersonFromLocation([FromRoute] int locationId, int personId)
        {
            var command = new UnassignResponsiblePersonFromLocationCommand { LocationId = locationId, UserProfileId = personId };

            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> Print([FromBody] PrintLocationResponsiblePersonsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}

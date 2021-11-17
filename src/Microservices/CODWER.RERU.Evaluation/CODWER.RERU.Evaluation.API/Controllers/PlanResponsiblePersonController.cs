using System.Collections.Generic;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.AssignResponsiblePersonToPlan;
using CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.GetNoAssignedResponsiblePersons;
using CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.GetPlanResponsiblePersons;
using CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.UnassignPlanResponsiblePerson;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanResponsiblePersonController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetPlanResponsiblePersons([FromRoute] GetPlanResponsiblePersonsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("no-assigned")]
        public async Task<List<UserProfileDto>> GetPersonsNotAssignedToPlan([FromQuery] GetNoAssignedResponsiblePersonsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> AssignPersonToPlan([FromBody] AssignResponsiblePersonToPlanCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Plan={planId}&&UserProfile={personId}")]
        public async Task<Unit> UnassignEvaluatorFromEvent([FromRoute] int planId, int personId)
        {
            var command = new UnassignPlanResponsiblePersonCommand { PlanId = planId, UserProfileId = personId };

            return await Mediator.Send(command);
        }
    }
}

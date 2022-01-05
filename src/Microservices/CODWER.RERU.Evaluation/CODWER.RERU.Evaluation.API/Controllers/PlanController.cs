using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Plans.AddPlan;
using CODWER.RERU.Evaluation.Application.Plans.DeletePlan;
using CODWER.RERU.Evaluation.Application.Plans.EditPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Plans.GetPlan;
using CODWER.RERU.Evaluation.Application.Plans.GetPlans;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Evaluation.Application.Plans.GetPlansByDate;
using CODWER.RERU.Evaluation.Application.Plans.GetCountedPlans;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<PlanDto> GetPlan([FromRoute] int id)
        {
            var query = new GetPlanQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<PlanDto>> GetPlans([FromQuery] GetPlansQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("byDate")]
        public async Task<PaginatedModel<PlanDto>> GetPlansByDate([FromQuery] GetPlansByDateQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("plansCount")]
        public async Task<List<PlanCount>> GetPlanCount([FromQuery] GetPlanCountQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddPlan([FromBody] AddPlanCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeletePlan([FromRoute] int id)
        {
            var command = new DeletePlanCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<int> UpdatePlan([FromBody] EditPlanCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

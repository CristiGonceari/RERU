using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Penalizations.AddPenalization;
using CODWER.RERU.Personal.Application.Penalizations.GetPenalization;
using CODWER.RERU.Personal.Application.Penalizations.GetPenalizations;
using CODWER.RERU.Personal.Application.Penalizations.RemovePenalization;
using CODWER.RERU.Personal.Application.Penalizations.UpdatePenalization;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenalizationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<PenalizationDto>> GetPenalizations([FromQuery] GetPenalizationsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<PenalizationDto> GetPenalization([FromRoute] int id)
        {
            var query = new GetPenalizationQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreatePenalization([FromBody] AddPenalizationCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdatePenalization([FromBody] UpdatePenalizationCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemovePenalization([FromRoute] int id)
        {
            var command = new RemovePenalizationCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}

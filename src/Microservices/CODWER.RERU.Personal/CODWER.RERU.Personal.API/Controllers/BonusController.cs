using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Bonuses.AddBonus;
using CODWER.RERU.Personal.Application.Bonuses.GetBonus;
using CODWER.RERU.Personal.Application.Bonuses.GetBonuses;
using CODWER.RERU.Personal.Application.Bonuses.RemoveBonus;
using CODWER.RERU.Personal.Application.Bonuses.UpdateBonus;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<BonusDto>> GetBonuses([FromQuery] GetBonusesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<BonusDto> GetBonus([FromRoute] int id)
        {
            var query = new GetBonusQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateBonus([FromBody] AddBonusCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateBonus([FromBody] UpdateBonusCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveBonus([FromRoute] int id)
        {
            var command = new RemoveBonusCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}

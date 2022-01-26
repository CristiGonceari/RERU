using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Ranks.AddRank;
using CODWER.RERU.Personal.Application.Ranks.GetRank;
using CODWER.RERU.Personal.Application.Ranks.GetRanks;
using CODWER.RERU.Personal.Application.Ranks.RemoveRank;
using CODWER.RERU.Personal.Application.Ranks.UpdateRank;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<RankDto>> GetRanks([FromQuery] GetRanksQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<RankDto> GetRank([FromRoute] int id)
        {
            var query = new GetRankQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateRank([FromBody] AddRankCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateRank([FromBody] UpdateRankCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveRank([FromRoute] int id)
        {
            var command = new RemoveRankCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}

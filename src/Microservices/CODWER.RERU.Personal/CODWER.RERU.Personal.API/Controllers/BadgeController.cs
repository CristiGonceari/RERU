using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Badges.AddBadge;
using CODWER.RERU.Personal.Application.Badges.GetBadge;
using CODWER.RERU.Personal.Application.Badges.GetBadges;
using CODWER.RERU.Personal.Application.Badges.RemoveBadge;
using CODWER.RERU.Personal.Application.Badges.UpdateBadge;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<BadgeDto>> GetBadges([FromQuery] GetBadgesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<BadgeDto> GetBadge([FromRoute] int id)
        {
            var query = new GetBadgeQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateBadge([FromBody] AddBadgeCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateBadge([FromBody] UpdateBadgeCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveBadge([FromRoute] int id)
        {
            var command = new RemoveBadgeCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}

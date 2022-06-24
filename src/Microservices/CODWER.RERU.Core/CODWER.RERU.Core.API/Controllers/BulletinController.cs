using CODWER.RERU.Core.Application.Bulletins.AddBulletin;
using CODWER.RERU.Core.Application.Bulletins.GetUserProfileBulletin;
using CODWER.RERU.Core.Application.Bulletins.UpdateBulletin;
using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class BulletinController : BaseController
    {
        public BulletinController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<BulletinDto> GetBulletinData([FromRoute] int id)
        {
            var query = new GetUserProfileBulletinQuery { UserProfileId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> AddBulletin([FromBody] BulletinDto data)
        {
            var command = new AddBulletinCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateBulletin([FromBody] BulletinDto data)
        {
            var command = new UpdateBulletinCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}

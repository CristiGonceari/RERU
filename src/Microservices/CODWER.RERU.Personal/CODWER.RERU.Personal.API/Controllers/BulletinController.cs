using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Bulletins.CreateBulletin;
using CODWER.RERU.Personal.Application.Bulletins.GetContractorBulletin;
using CODWER.RERU.Personal.Application.Bulletins.UpdateBulletin;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<BulletinsDataDto> GetBulletinData([FromRoute] int id)
        {
            var query = new GetContractorBulletinQuery{ ContractorId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateBulletin([FromBody] BulletinsDataDto data)
        {
            var command = new CreateBulletinCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateBulletin([FromBody] BulletinsDataDto data)
        {
            var command = new UpdateBulletinCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}

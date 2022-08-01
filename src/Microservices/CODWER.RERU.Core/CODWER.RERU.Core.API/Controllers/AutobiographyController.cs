using CODWER.RERU.Core.Application.Autobiographies.AddAutobiography;
using CODWER.RERU.Core.Application.Autobiographies.GetUserProfileAutobiography;
using CODWER.RERU.Core.Application.Autobiographies.UpdateAutobiography;
using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutobiographyController : BaseController
    {
        public AutobiographyController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<AutobiographyDto> GetAutobiography([FromRoute] int id)
        {
            var query = new GetUserProfileAutobiographyQuery { ContractorId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> AddAutobiography([FromBody] AutobiographyDto data)
        {
            var command = new AddAutobiographyCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateAutobiography([FromBody] AutobiographyDto data)
        {
            var command = new UpdateAutobiographyCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}

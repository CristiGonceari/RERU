using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Autobiographies.AddAutobiography;
using CODWER.RERU.Personal.Application.Autobiographies.GetContractorAutobiography;
using CODWER.RERU.Personal.Application.Autobiographies.UpdateAutobiography;
using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutobiographyController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<AutobiographyDto> GetAutobiography([FromRoute] int id)
        {
            var query = new GetContractorAutobiographyQuery { ContractorId = id };
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

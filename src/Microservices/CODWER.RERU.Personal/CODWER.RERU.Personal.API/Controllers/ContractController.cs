using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Contracts.AddContract;
using CODWER.RERU.Personal.Application.Contracts.GetContract;
using CODWER.RERU.Personal.Application.Contracts.UpdateContract;
using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : BaseController
    {
        [HttpGet("{contractorId}")]
        public async Task<IndividualContractDetails> GetIndividualContract([FromRoute] int contractorId)
        {
            var command = new GetContractQuery { ContractorId = contractorId };

            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<int> CreateIndividualContract([FromBody] AddIndividualContractDto dto)
        {
            var command = new AddContractCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task UpdateIndividualContract([FromBody] UpdateIndividualContractDto dto)
        {
            var command = new UpdateContractCommand(dto);

            await Mediator.Send(command);
        }
    }
}

using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.MaterialStatuses.AddMaterialStatus;
using CODWER.RERU.Personal.Application.MaterialStatuses.GetContractorMaterialStatus;
using CODWER.RERU.Personal.Application.MaterialStatuses.UpdateMaterialStatus;
using CODWER.RERU.Personal.DataTransferObjects.MaterialStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialStatusController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<AddEditMaterialStatusDto> GetMaterialStatus([FromRoute] int id)
        {
            var query = new GetContractorMaterialStatusQuery { ContractorId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateMaterialStatus([FromBody] AddEditMaterialStatusDto data)
        {
            var command = new AddMaterialStatusCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateMaterialStatus([FromBody] AddEditMaterialStatusDto data)
        {
            var command = new UpdateMaterialStatusCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}

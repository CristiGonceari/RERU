using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Attestations.AddAttestation;
using CODWER.RERU.Personal.Application.Attestations.GetAttestation;
using CODWER.RERU.Personal.Application.Attestations.GetAttestations;
using CODWER.RERU.Personal.Application.Attestations.RemoveAttestation;
using CODWER.RERU.Personal.Application.Attestations.UpdateAttestation;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttestationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<AttestationDto>> GetAttestations([FromQuery] GetAttestationsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<AttestationDto> GetAttestation([FromRoute] int id)
        {
            var query = new GetAttestationQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateAttestation([FromBody] AddAttestationCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateAttestation([FromBody] UpdateAttestationCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveAttestation([FromRoute] int id)
        {
            var command = new RemoveAttestationCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}

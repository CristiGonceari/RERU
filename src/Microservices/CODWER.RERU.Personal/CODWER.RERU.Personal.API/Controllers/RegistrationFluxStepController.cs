using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.RegistrationFluxSteps.AddRegistrationFluxStep;
using CODWER.RERU.Personal.Application.RegistrationFluxSteps.GetContractorRegistrationFluxSteps;
using CODWER.RERU.Personal.Application.RegistrationFluxSteps.UpdateRegistrationFluxStep;
using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationFluxStepController : BaseController
    {
        [HttpGet]
        public async Task<List<RegistrationFluxStepDto>> GetRegistrationFluxStep([FromQuery] GetContractorRegistrationFluxStepsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateRegistrationFluxStep([FromBody] RegistrationFluxStepDto dto)
        {
            var command = new AddRegistrationFluxStepCommand(dto)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateRegistrationFluxStep([FromBody] RegistrationFluxStepDto dto)
        {
            var command = new UpdateRegistrationFluxStepCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}

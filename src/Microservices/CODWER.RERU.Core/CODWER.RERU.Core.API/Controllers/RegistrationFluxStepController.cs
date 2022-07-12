using CODWER.RERU.Core.Application.RegistrationFluxSteps.AddRegistrationFluxStep;
using CODWER.RERU.Core.Application.RegistrationFluxSteps.GetUserProfileRegistrationFluxSteps;
using CODWER.RERU.Core.Application.RegistrationFluxSteps.UpdateRegistrationFluxStep;
using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationFluxStepController : BaseController
    {
        public RegistrationFluxStepController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<List<RegistrationFluxStepDto>> GetRegistrationFluxStep([FromQuery] GetUserProfileRegistrationFluxStepsQuery query)
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

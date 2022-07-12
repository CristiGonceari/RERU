using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using MediatR;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps.UpdateRegistrationFluxStep
{
    public class UpdateRegistrationFluxStepCommand : IRequest<Unit>
    {
        public UpdateRegistrationFluxStepCommand(RegistrationFluxStepDto dto)
        {
            Data = dto;
        }

        public RegistrationFluxStepDto Data { get; set; }
    }
}

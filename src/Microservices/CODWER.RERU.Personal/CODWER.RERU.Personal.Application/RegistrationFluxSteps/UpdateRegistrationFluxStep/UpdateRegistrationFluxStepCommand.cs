using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using MediatR;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps.UpdateRegistrationFluxStep
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

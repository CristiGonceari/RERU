using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using MediatR;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps.AddRegistrationFluxStep
{
    public class AddRegistrationFluxStepCommand : IRequest<int>
    {
        public AddRegistrationFluxStepCommand(RegistrationFluxStepDto data)
        {
            Data = data;
        }

        public RegistrationFluxStepDto Data { get; set; }
    }
}

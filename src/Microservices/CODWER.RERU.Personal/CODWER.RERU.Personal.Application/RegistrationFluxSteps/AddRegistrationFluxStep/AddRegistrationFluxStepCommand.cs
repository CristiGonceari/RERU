using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using MediatR;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps.AddRegistrationFluxStep
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

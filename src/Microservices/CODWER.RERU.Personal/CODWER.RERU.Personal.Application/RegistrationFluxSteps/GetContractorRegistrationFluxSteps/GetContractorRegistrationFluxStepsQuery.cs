using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using RERU.Data.Entities.Enums;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps.GetContractorRegistrationFluxSteps
{
    public class GetContractorRegistrationFluxStepsQuery : IRequest<List<RegistrationFluxStepDto>>
    {
        public int ContractorId { get; set; }
        public RegistrationFluxStepEnum? Step { get; set; }
    }
}

using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using RERU.Data.Entities.Enums;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps.GetUserProfileRegistrationFluxSteps
{
    public class GetUserProfileRegistrationFluxStepsQuery : IRequest<List<RegistrationFluxStepDto>>
    {
        public int ContractorId { get; set; }
        public RegistrationFluxStepEnum? Step { get; set; }
    }
}

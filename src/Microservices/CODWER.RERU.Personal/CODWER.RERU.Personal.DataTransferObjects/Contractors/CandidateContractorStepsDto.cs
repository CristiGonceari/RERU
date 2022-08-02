using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class CandidateContractorStepsDto
    {
        public int ContractorId { get; set; }

        public List<int> UnfinishedSteps { get; set; }
        public List<CheckedRegistrationFluxStepsDto> CheckedSteps { get; set; }
    }
}

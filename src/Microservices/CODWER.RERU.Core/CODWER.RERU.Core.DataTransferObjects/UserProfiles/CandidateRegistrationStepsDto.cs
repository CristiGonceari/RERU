using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using System.Collections.Generic;

namespace CODWER.RERU.Core.DataTransferObjects.UserProfiles
{
    public class CandidateRegistrationStepsDto
    {
        public int UserProfileId { get; set; }

        public List<int> UnfinishedSteps { get; set; } 
        public List<CheckedRegistrationFluxStepsDto> CheckedSteps { get; set; }
    }
}

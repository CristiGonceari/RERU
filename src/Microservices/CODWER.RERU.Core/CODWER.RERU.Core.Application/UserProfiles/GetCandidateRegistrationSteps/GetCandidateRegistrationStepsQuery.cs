using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateRegistrationSteps
{
    public class GetCandidateRegistrationStepsQuery : IRequest<CandidateRegistrationStepsDto>
    {
    }
}

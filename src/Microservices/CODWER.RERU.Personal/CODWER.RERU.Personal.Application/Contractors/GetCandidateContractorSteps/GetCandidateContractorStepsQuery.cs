using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetCandidateContractorSteps
{
    public class GetCandidateContractorStepsQuery : IRequest<CandidateContractorStepsDto>
    {
        public int ContractorId { get; set; }
    }
}

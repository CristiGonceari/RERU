using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.GetMySolicitedTest
{
    public class GetMySolicitedTestQuery : IRequest<SolicitedCandidatePositionDto>
    {
        public int Id { get; set; }
    }
}

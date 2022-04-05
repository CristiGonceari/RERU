using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.GetMySolicitedTest
{
    public class GetMySolicitedTestQuery : IRequest<SolicitedTestDto>
    {
        public int Id { get; set; }
    }
}

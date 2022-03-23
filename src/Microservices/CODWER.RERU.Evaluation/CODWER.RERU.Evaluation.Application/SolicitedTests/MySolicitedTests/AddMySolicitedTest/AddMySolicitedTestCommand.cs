using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.AddMySolicitedTest
{
    public class AddMySolicitedTestCommand : IRequest<int>
    {
        public AddEditSolicitedTestDto Data { get; set; }
    }
}

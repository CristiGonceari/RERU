using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.AddMySolicitedTest
{
    public class AddMySolicitedTestCommand : IRequest<AddSolicitedCandidatePositionResponseDto>
    {
        public AddEditSolicitedTestDto Data { get; set; }
    }
}

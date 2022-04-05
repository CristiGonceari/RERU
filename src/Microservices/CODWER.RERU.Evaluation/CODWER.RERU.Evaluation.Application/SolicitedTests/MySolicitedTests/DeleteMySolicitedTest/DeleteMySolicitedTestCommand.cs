using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.DeleteMySolicitedTest
{
    public class DeleteMySolicitedTestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

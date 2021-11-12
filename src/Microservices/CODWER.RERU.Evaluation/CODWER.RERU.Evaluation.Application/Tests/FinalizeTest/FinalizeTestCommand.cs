using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeTest
{
    public class FinalizeTestCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}

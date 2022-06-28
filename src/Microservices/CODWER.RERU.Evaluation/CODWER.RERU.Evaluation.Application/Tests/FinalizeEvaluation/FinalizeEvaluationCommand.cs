using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeEvaluation
{
    public class FinalizeEvaluationCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}

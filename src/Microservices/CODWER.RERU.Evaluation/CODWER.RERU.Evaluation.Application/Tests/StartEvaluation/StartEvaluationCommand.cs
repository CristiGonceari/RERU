using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.StartEvaluation
{
    public class StartEvaluationCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}

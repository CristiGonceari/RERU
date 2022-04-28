using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.StartTest
{
    public class StartTestCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}

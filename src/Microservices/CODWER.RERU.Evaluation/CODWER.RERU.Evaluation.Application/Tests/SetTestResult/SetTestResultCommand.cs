using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.SetTestResult
{
    public class SetTestResultCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
        public TestResultStatusEnum ResultStatus { get; set; }
        public int[] RecommendedFor { get; set; }
        public int[] NotRecommendedFor { get; set; }
    }
}

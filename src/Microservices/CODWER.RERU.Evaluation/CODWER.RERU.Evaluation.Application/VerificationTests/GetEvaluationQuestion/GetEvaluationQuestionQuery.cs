using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetEvaluationQuestion
{
    public class GetEvaluationQuestionQuery : IRequest<VerificationTestQuestionUnitDto>
    {
        public int TestId { get; set; }
        public int QuestionIndex { get; set; }
    }
}

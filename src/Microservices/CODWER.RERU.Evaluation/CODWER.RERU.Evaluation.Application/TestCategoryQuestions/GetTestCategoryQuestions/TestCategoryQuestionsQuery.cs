using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions
{
    public class TestCategoryQuestionsQuery : IRequest<TestCategoryQuestionContentDto>
    {
        public int TestTypeQuestionCategoryId { get; set; }
    }
}

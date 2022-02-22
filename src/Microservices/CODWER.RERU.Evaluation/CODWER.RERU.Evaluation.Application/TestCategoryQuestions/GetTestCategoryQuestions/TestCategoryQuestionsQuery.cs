using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions
{
    public class TestCategoryQuestionsQuery : IRequest<TestCategoryQuestionContentDto>
    {
        public int TestTemplateQuestionCategoryId { get; set; }
    }
}

using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestions
{
    public class GetTestQuestionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestQuestionDto>>
    {
        public int TestId { get; set; }
    }
}

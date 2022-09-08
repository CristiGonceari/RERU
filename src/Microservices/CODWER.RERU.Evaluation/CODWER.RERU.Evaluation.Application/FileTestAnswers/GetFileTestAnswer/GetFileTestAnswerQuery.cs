using CODWER.RERU.Evaluation.DataTransferObjects.FileTestAnswers;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.GetFileTestAnswer
{
    public class GetFileTestAnswerQuery : IRequest<GetTestFileDto>
    {
        public int QuestionIndex { get; set; }
        public int TestId { get; set; }
    }
}

using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.AddFileTestAnswer
{
    public class AddFileTestAnswerCommand : IRequest<string>
    {
        public int QuestionIndex { get; set; }
        public int TestId { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}

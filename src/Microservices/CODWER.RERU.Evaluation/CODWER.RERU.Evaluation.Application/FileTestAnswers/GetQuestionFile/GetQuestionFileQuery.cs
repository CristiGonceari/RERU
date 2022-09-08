using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.GetQuestionFile
{
    public class GetQuestionFileQuery : IRequest<FileDataDto>
    {
        public string FileId { get; set; }
    }
}

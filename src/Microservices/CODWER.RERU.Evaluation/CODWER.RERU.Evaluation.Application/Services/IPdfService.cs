using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IPdfService
    {
        public Task<FileDataDto> PrintTestTemplatePdf(int testTemplateId);
        public Task<FileDataDto> PrintTestPdf(int testId);
        public Task<FileDataDto> PrintPerformingTestPdf(int testId);
        public Task<FileDataDto> PrintQuestionUnitPdf(int questionId);
    }
}

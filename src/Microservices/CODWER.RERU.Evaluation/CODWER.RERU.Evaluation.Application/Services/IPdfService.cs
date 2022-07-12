using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IPdfService
    {
        public Task<FileDataDto> PrintTestTemplatePdf(int testTemplateId);
        public Task<FileDataDto> PrintTestPdf(int testId);
        public Task<FileDataDto> PrintTestResultPdf(int testId);
        public Task<FileDataDto> PrintEvaluationResultPdf(int testId);
        public Task<FileDataDto> PrintPerformingTestPdf(List<int> testsIds);
        public Task<FileDataDto> PrintQuestionUnitPdf(int questionId);
    }
}

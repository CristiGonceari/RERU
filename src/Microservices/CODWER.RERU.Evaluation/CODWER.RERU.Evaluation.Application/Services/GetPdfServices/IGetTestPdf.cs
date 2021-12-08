using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices
{
    public interface IGetTestPdf
    {
        public Task<FileDataDto> PrintTestPdf(int testId);
    }
}

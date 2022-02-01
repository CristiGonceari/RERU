using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices
{
    public interface IGetTestPdf
    {
        public Task<FileDataDto> PrintTestPdf(int testId);
    }
}

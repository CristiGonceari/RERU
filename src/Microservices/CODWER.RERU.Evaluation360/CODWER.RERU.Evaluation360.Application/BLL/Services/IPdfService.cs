using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Evaluation360.Application.BLL.Services
{
    public interface IPdfService
    {
        public Task<FileDataDto> PrintEvaluationPdf(int evaluationId);
    }
}
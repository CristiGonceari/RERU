using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices
{
    public interface IGetTestTemplatePdf
    {
        public Task<FileDataDto> PrintTestTemplatePdf(int testTemplateId);
    }
}

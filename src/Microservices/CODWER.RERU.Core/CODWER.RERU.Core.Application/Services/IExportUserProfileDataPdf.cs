using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Core.Application.Services.Implementations
{
    public interface IExportUserProfileDataPdf
    {
        public Task<FileDataDto> ExportUserProfileDatasPdf(int userProfileId);
    }
}
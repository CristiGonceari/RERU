using CVU.ERP.Common.DataTransferObjects.Files;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Services
{
    public interface IExportUserProfileData
    {
        public Task<FileDataDto> ExportUserProfileDatas(int userProfileId);
    }
}

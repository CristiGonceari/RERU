using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IImportEmployeeFunctionsService
    {
        public Task<FileDataDto> Import(IFormFile data);
    }
}

using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IImportDepartmentsAndRolesService
    {
        public Task<FileDataDto> Import(IFormFile data, ImportTypeEnum type);
    }
}

using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IImportDepartmentOrganizationalChartService
    {
        public Task<FileDataDto> ImportDepartmentToDepartmentRelation(IFormFile data, int organizationalChartId);

    }
}

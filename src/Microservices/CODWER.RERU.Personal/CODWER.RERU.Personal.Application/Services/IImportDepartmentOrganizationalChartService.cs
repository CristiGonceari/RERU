using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IImportDepartmentOrganizationalChartService
    {
        public Task<ImportDepartmentRoleRelationDto> ImportDepartmentToDepartmentRelation(IFormFile data, int organizationalChartId);

    }
}

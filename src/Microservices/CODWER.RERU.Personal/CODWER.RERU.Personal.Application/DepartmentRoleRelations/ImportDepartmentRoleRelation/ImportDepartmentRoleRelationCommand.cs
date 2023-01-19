using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.ImportDepartmentRoleRelation
{
    public class ImportDepartmentRoleRelationCommand : IRequest<FileDataDto>
    {
        public ExcelDataDto Data { get; set; }
        public int OrganizationalChartId { get; set; }
    }
}

using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.ImportOrganizationRoles
{
    public class ImportRolesCommand : IRequest<FileDataDto>
    {
        public ExcelDataDto Data { get; set; }
    }
}

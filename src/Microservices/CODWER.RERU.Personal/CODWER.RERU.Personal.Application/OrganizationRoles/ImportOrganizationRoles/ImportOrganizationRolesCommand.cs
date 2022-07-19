using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.ImportOrganizationRoles
{
    public class ImportRolesCommand : IRequest<Unit>
    {
        public ExcelDataDto Data { get; set; }
    }
}

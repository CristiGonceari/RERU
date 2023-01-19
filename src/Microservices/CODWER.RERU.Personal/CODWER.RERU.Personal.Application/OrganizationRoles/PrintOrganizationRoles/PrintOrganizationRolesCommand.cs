using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.PrintOrganizationRoles
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ROLURI)]

    public class PrintOrganizationRolesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortCode { get; set; }

        public string SearchWord { get; set; }
    }
}

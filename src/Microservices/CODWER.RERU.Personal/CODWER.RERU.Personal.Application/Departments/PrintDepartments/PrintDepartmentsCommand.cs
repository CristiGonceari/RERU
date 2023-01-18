using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.PrintDepartments
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE)]

    public class PrintDepartmentsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}

using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.PrintDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class PrintDepartmentCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}

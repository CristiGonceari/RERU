using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.PrintEmployeeFunctions
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_FUNCTII)]

    public class PrintEmployeeFunctionsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string SearchWord { get; set; }
    }
}

using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.ImportEmployeeFunctions
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_FUNCTII)]

    public class ImportEmployeeFunctionsCommand : IRequest<FileDataDto>
    {
        public ExcelDataDto Data { get; set; }
    }
}

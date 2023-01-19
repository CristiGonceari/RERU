using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunction
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_FUNCTII)]

    public class GetEmployeeFunctionQuery : IRequest<EmployeeFunctionDto>
    {
        public int Id { get; set; }
    }
}

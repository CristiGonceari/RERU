using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.AddDepartment
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENTS_GENERAL_ACCESS)]

    public class AddDepartmentCommand : IRequest<int>
    {
        public AddEditDepartmentDto Data { get; set; }
    }
}

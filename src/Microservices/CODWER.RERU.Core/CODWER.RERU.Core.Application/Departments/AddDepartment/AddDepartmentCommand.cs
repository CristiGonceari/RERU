using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.AddDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class AddDepartmentCommand : IRequest<int>
    {
        public DepartmentDto Data { get; set; }
    }
}

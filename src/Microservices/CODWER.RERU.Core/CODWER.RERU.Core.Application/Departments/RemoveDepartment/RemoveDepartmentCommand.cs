using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.RemoveDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class RemoveDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

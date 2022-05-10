using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.AddDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class AddDepartmentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
    }
}

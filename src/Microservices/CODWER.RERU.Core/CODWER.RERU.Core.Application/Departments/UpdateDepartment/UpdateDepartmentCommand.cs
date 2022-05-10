using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.UpdateDepartment
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class UpdateDepartmentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
    }
}

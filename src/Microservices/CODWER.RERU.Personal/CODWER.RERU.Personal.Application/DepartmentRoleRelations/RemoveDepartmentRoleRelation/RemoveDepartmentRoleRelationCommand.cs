using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.RemoveDepartmentRoleRelation
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENT_ROLE_RELATIONS_GENERAL_ACCESS)]

    public class RemoveDepartmentRoleRelationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

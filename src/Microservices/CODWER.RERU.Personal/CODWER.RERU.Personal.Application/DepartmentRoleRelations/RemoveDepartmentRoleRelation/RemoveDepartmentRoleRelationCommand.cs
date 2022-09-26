using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.RemoveDepartmentRoleRelation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_RELATIA_DEPARTAMENT_ROL)]

    public class RemoveDepartmentRoleRelationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

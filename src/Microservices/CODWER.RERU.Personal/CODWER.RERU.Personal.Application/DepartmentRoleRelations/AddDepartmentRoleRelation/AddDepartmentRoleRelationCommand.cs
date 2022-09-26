using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Add;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddDepartmentRoleRelation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_RELATIA_DEPARTAMENT_ROL)]

    public class AddDepartmentRoleRelationCommand : IRequest<int>
    {
        public AddDepartmentRoleRelationDto Data { get; set; }
    }
}
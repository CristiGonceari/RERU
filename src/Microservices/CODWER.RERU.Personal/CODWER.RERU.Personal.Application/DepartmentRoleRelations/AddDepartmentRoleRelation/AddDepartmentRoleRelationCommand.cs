using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Add;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddDepartmentRoleRelation
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENT_ROLE_RELATIONS_GENERAL_ACCESS)]

    public class AddDepartmentRoleRelationCommand : IRequest<int>
    {
        public AddDepartmentRoleRelationDto Data { get; set; }
    }
}
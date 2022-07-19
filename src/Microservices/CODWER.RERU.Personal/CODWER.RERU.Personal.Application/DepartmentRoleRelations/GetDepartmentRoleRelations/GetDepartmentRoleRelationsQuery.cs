using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetDepartmentRoleRelations
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENT_ROLE_RELATIONS_GENERAL_ACCESS)]

    public class GetDepartmentRoleRelationsQuery : IRequest<DepartmentRoleRelationDto>
    {
        public int? ParentDepartmentId { get; set; }
        public int? ParentRoleId { get; set; }
    }
}

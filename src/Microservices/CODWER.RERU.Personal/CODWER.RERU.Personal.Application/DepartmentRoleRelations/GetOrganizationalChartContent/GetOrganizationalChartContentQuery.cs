using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetOrganizationalChartContent
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENT_ROLE_RELATIONS_GENERAL_ACCESS)]

    public class GetOrganizationalChartContentQuery : IRequest<OrganizationalChartContentDto>
    {
        public int OrganizationalChartId { get; set; }
    }
}

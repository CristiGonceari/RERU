using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetOrganizationalChartContent
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_RELATIA_DEPARTAMENT_ROL)]

    public class GetOrganizationalChartContentQuery : IRequest<OrganizationalChartContentDto>
    {
        public int OrganizationalChartId { get; set; }
    }
}

using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddOrganizationalChartHead
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_RELATIA_DEPARTAMENT_ROL)]

    public class AddOrganizationalChartHeadCommand : IRequest<int>
    {
        public int HeadId { get; set; }
        public OrganizationalChartItemType? Type { get; set; }
        public int OrganizationalChartId { get; set; }
    }
}

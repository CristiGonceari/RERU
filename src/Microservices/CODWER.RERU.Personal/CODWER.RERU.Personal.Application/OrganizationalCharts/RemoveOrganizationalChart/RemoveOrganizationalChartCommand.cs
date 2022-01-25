using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.RemoveOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATIONAL_CHART_GENERAL_ACCESS)]

    public class RemoveOrganizationalChartCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

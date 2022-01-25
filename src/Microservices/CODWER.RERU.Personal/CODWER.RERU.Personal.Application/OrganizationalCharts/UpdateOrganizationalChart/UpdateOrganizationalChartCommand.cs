using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.UpdateOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATIONAL_CHART_GENERAL_ACCESS)]

    public class UpdateOrganizationalChartCommand : IRequest<Unit>
    {
        public AddEditOrganizationalChartDto Data { get; set; }
    }
}

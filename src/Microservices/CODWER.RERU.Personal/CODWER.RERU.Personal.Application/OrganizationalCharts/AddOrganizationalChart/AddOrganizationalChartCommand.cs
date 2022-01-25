using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATIONAL_CHART_GENERAL_ACCESS)]

    public class AddOrganizationalChartCommand : IRequest<int>
    {
        public AddEditOrganizationalChartDto Data { get; set; }
    }
}

using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATIONAL_CHART_GENERAL_ACCESS)]

    public class GetOrganizationalChartQuery : IRequest<OrganizationalChartDto>
    {
        public int Id { get; set; }
    }
}

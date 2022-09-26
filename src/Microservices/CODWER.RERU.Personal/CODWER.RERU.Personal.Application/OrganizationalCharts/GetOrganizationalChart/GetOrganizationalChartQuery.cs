using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ORGANIGRAMA)]

    public class GetOrganizationalChartQuery : IRequest<OrganizationalChartDto>
    {
        public int Id { get; set; }
    }
}

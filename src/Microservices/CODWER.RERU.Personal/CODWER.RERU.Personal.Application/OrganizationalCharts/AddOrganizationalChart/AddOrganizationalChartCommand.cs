using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ORGANIGRAMA)]

    public class AddOrganizationalChartCommand : IRequest<int>
    {
        public AddEditOrganizationalChartDto Data { get; set; }
    }
}

using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.UpdateOrganizationalChart
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ORGANIGRAMA)]

    public class UpdateOrganizationalChartCommand : IRequest<Unit>
    {
        public AddEditOrganizationalChartDto Data { get; set; }
    }
}

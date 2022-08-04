using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationRoles
{
    public class GetRolesChartQuery : IRequest<List<SelectItem>>
    {
        public int OrganizationalChartId { get; set; }
    }
}

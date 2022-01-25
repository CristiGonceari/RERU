using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetDepartments
{
    public class GetDepartmentsChartQuery : IRequest<List<SelectItem>>
    {
        public int OrganizationalChartId { get; set; }
    }
}

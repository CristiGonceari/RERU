using CVU.ERP.OrganigramService.Enums;
using CVU.ERP.OrganigramService.Models;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.Organigrams.GetOrganigramContent
{
    public class GetOrganigramContentQuery : IRequest<List<OrganigramContent>>
    {
        public int ParentDepartmentId { get; set; }
        public OrganizationalChartItemType Type { get; set; }
        public int OrganigramId { get; set; }
    }
}

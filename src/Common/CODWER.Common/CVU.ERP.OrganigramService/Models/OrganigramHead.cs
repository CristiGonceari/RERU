using CVU.ERP.OrganigramService.Enums;

namespace CVU.ERP.OrganigramService.Models
{
    public class OrganigramHead
    {
        public int OrganigramId { get; set; }
        public int ParentDepartmentId { get; set; }
        public string OrganigramHeadName { get; set; }
        public OrganizationalChartItemType Type { get; set; }
    }
}

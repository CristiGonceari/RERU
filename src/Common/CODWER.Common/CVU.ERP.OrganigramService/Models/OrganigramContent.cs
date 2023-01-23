using CVU.ERP.OrganigramService.Enums;

namespace CVU.ERP.OrganigramService.Models
{
    public class OrganigramContent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RelationId { get; set; }

        public OrganizationalChartItemType Type { get; set; }
    }
}

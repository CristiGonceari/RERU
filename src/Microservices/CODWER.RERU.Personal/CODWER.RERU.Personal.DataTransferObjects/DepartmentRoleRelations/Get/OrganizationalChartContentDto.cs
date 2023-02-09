using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get
{
    public class OrganizationalChartContentDto
    {
        public OrganizationalChartContentDto()
        {
            Childs = new List<OrganizationalChartContentDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int RelationId { get; set; }
        public bool IsHead { get; set; }

        public OrganizationalChartItemType Type { get; set; }

        public List<OrganizationalChartContentDto> Childs { get; set; }
    }
}

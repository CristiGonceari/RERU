using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations
{
    public abstract class DepartmentRoleRelation : SoftDeleteBaseEntity
    {
        public int OrganizationalChartId { get; set; }
        public OrganizationalChart OrganizationalChart { get; set; }
    }
}

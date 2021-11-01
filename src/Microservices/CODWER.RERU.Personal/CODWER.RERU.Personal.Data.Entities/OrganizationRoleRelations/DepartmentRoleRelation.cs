using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations
{
    public abstract class DepartmentRoleRelation : SoftDeleteBaseEntity
    {
        public int OrganizationalChartId { get; set; }
        public OrganizationalChart OrganizationalChart { get; set; }
    }
}

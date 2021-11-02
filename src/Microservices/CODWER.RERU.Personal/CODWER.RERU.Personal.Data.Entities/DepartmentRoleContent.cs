using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class DepartmentRoleContent : SoftDeleteBaseEntity
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int OrganizationRoleId { get; set; }
        public OrganizationRole OrganizationRole { get; set; }

        public int OrganizationRoleCount { get; set; }
    }
}

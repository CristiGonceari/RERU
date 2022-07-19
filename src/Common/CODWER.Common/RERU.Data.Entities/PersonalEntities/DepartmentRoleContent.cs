using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class DepartmentRoleContent : SoftDeleteBaseEntity
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int RoleCount { get; set; }
    }
}

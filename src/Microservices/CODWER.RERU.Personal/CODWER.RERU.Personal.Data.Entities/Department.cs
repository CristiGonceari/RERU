using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class Department : SoftDeleteBaseEntity
    {
        public Department()
        {
            DepartmentRoleContents = new HashSet<DepartmentRoleContent>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DepartmentRoleContent> DepartmentRoleContents { get; set; }
    }
}

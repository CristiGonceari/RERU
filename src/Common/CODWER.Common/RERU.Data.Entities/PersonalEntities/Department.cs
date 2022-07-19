using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Department : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
        public virtual ICollection<DepartmentRoleContent> DepartmentRoleContents { get; set; }
    }
}

using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Role : SoftDeleteBaseEntity
    {
        public Role()
        {
            DepartmentRoleContents = new HashSet<DepartmentRoleContent>();
        }

        public string Name { get; set; }
        public int ColaboratorId { get; set; }
        public string Code { get; set; }
        public string ShortCode { get; set; }

        public virtual ICollection<DepartmentRoleContent> DepartmentRoleContents { get; set; }
    }
}

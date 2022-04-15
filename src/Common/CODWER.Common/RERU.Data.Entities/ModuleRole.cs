using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class ModuleRole : SoftDeleteBaseEntity {
        public int ModuleId { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public bool IsAssignByDefault { set; get; }
        public RoleTypeEnum Type { set; get; }
        public Module Module { set; get; }

        public List<ModuleRolePermission> Permissions { set; get; }
    }
}
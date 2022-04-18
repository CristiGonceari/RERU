using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ModuleRolePermission : BaseEntity {
        public int ModuleRoleId { set; get; }
        public int ModulePermissionId { set; get; }
        public ModuleRole Role { set; get; }
        public ModulePermission Permission { set; get; }
    }
}